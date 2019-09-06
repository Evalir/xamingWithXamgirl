using System;
using System.Globalization;
using Xamarin.Forms;

namespace MVMMLogin.Converters
{
    public class LastNameConverter : IValueConverter
    {
        public LastNameConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Last Name: {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
