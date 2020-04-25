using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TEST.Helpers
{
    public static class CollectionsHelper
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> collectionToAdd)
        {
            foreach(T addItem in collectionToAdd)
            {
                collection.Add(addItem);
            }
        }
        public static void RemoveRange<T>(this Collection<T> collection, IEnumerable<T> collectionToRemove)
        {
            foreach (T removeItem in collectionToRemove)
            {
                collection.Remove(removeItem);
            }
        }
    }
}
