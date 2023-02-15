using System.Windows;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;

namespace SupplyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Dispatcher MainDispatcher { get; set; } = null!;

        public App()
        {
            using ApplicationContext context = new();

            context.Database.Migrate();
        }
    }
}
