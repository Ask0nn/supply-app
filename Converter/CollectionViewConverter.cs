using SupplyApp.Entity;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace SupplyApp.Converter
{
    internal class CollectionViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ObservableCollection<Request> data) return new ObservableCollection<Request>();
            var list = new ListCollectionView(data);
            list.MoveCurrentToPosition(-1);
            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
