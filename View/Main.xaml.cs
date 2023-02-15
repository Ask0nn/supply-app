using MaterialDesignThemes.Wpf;
using SupplyApp.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Wpf.Ui.Controls;

namespace SupplyApp.View
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UiWindow
    {
        public Main()
        {
            this.Language = XmlLanguage.GetLanguage("ru-RU");

            InitializeComponent();

            App.MainDispatcher = this.Dispatcher;

            PaletteHelper paletteHelper = new();
            ITheme theme = paletteHelper.GetTheme();
            if (Properties.Settings.Default.IsDark)
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                theme.SetBaseTheme(Theme.Dark);
            }
            else
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                theme.SetBaseTheme(Theme.Light);
            }
            paletteHelper.SetTheme(theme);
        }

        private void TitleBar_OnHelpClicked(object sender, RoutedEventArgs e)
        {
            DialogController.ShowInfo();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DataGrid.SelectedIndex = -1;
            e.Handled = true;
        }
    }
}
