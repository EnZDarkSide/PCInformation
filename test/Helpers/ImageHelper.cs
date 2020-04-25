using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF_TEST.Helpers
{
    public static class ImageHelper
    {
        // Wpf плохо работает с Icon, поэтому создаем картинку
        public static BitmapFrame IcoToImageSource(this Icon icon)
        {
            var stream = new MemoryStream();
            Bitmap bmp = icon.ToBitmap();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return BitmapFrame.Create(stream);
        }
    }
}
