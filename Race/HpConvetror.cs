using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;


namespace Race
{
    public class HpConvetror : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
