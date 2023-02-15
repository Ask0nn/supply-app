using System;
using System.Globalization;
using System.Windows.Data;

namespace SupplyApp.Converter
{
    internal class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is long time and > 0 ? DateTimeOffset.FromUnixTimeSeconds(time).DateTime : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime { Year: > 1970 } time ? new DateTimeOffset(time.AddDays(1)).ToUnixTimeSeconds() : value;
        }
    }
}
