using System;
using System.Globalization;
using Xamarin.Forms;

namespace DockpadAPI.Converters
{
    public class IntToMoodConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mood = value as int?;
            if (mood == 5)
            {
                return "Great";
            }
            else if (mood == 4)
            {
                return "Good";
            }
            else if (mood == 3)
            {
                return "Normal";
            }
            else if (mood == 2)
            {
                return "Not Good";
            }
            else if (mood == 1)
            {
                return "Bad";
            }
            return "Mistake";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
