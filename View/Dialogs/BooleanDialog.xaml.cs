using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupplyApp.View.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для BooleanDialog.xaml
    /// </summary>
    public partial class BooleanDialog : UserControl
    {
        public BooleanDialog(string message = "Вы уверены ?")
        {
            InitializeComponent();
            MessageTxt.Text = message;
        }
    }
}
