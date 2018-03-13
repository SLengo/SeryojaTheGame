using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Race
{
    public class DataConvertors : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
