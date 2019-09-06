using System;
using System.Globalization;
using Xamarin.Forms;

namespace MVMMLogin.Converters
{
    public class PhotoToPlaceholderConverter : IValueConverter
    {
        public PhotoToPlaceholderConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "camera" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
