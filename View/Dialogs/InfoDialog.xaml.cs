using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SupplyApp.View.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для InfoDialog.xaml
    /// </summary>
    public partial class InfoDialog : UserControl
    {
        public InfoDialog()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(e.Uri.AbsoluteUri)
            {
                UseShellExecute = true
            };
            p.Start();
            e.Handled = true;
        }
    }
}
