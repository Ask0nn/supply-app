using SupplyApp.Entity;
using System.Collections.Generic;
using System.Windows.Controls;
using SupplyApp.ViewModel;

namespace SupplyApp.View.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для FilesDialog.xaml
    /// </summary>
    public partial class FilesDialog : UserControl
    {
        public FilesDialog(long requestId)
        {
            InitializeComponent();
            this.DataContext = new FilesViewModel(requestId);
        }
    }
}
