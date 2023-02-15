using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using SupplyApp.Entity;
using SupplyApp.ViewModel;

namespace SupplyApp.View.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для RequestDialog.xaml
    /// </summary>
    public partial class RequestDialog : UserControl
    {
        public RequestDialog(Request request)
        {
            InitializeComponent();
            DataContext = new RequestViewModel(request);
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
