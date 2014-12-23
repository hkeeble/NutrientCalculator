using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace Assignment
{
    /// <summary>
    /// This code was taken from a module workshop task.
    /// </summary>
    public class ImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            MemoryStream memStream = new MemoryStream((byte[])value);
            memStream.Seek(0, SeekOrigin.Begin);
            BitmapImage cloudImage = new BitmapImage();
            cloudImage.DecodePixelWidth = 480;
            cloudImage.SetSource(memStream);
            memStream.Dispose();

            return cloudImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
