using System;
using System.Globalization;
using Xamarin.Forms;

namespace MVMMLogin.Converters
{
    public class FirstNameConverter :IValueConverter
    {
        public FirstNameConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"First Name: {value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
