using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SupplyApp.Converter
{
    internal class HexColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string hex
                ? (SolidColorBrush)new BrushConverter().ConvertFrom(hex)!
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is SolidColorBrush solid ? solid.Color.ToString() : value;
        }
    }
}
