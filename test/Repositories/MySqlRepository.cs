using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;
using WPF_TEST.Models;

namespace WPF_TEST.Repositories
{
    static class MySqlRepository
    {
        static MySqlConnection connection = new MySqlConnection("server=localhost;userid=root;database=PCInfo");

        public static void SaveReport(List<App> appList)
        {
            connection.Open();
            if (connection != null && connection.State == ConnectionState.Open)
            { 
                string newTableName = DateTime.Now.ToString();
                MySqlCommand createTableSqlCmd = new MySqlCommand($"CREATE TABLE `{newTableName}` (AppName varchar(255) NOT NULL, ElapsedTime time NOT NULL);", connection);
                createTableSqlCmd.ExecuteNonQuery();

                string command = $"INSERT INTO `{newTableName}` (AppName, ElapsedTime) VALUES {string.Join(",", appList.Select(app => $"('{app.Process.ProcessName}', '{app.Elapsed}')"))};";
                MySqlCommand cmd = new MySqlCommand(command, connection);
                int rowCount = cmd.ExecuteNonQuery();

                MessageBox.Show($"Добавлено значений : {rowCount}");
                connection.Close();
            }
        }
    }
}
