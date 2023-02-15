using SupplyApp.Entity;
using SupplyApp.ViewModel;
using System.Windows.Controls;

namespace SupplyApp.View.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для FilterDialog.xaml
    /// </summary>
    public partial class FilterDialog : UserControl
    {
        public FilterDialog(Filter filter)
        {
            InitializeComponent();
            DataContext = new FilterViewModel(filter);
        }
    }
}
