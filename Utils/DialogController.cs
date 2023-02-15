using MaterialDesignThemes.Wpf;
using SupplyApp.View.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using SupplyApp.Entity;

namespace SupplyApp.Utils
{
    internal class DialogController
    {
        private const string DialogId = "Dialog";

        public static async void ShowAlert(string? message = null)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (message is null)
                    await DialogHost.Show(new AlertDialog(), DialogId);
                else
                    await DialogHost.Show(new AlertDialog(message), DialogId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }
        }

        public static async Task<bool> ShowBoolean(string? message = null)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                var result = message is null ? 
                    await DialogHost.Show(new BooleanDialog(), DialogId) :
                    await DialogHost.Show(new BooleanDialog(message), DialogId);
                return (bool) (result ?? false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }

            return false;
        }

        public static bool ShowDir(string? dirName)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                UserControl dirView;
                switch (dirName)
                {
                    case "Executor":
                        dirView = new ExecutorDirDialog();
                        break;
                    case "Initiator":
                        dirView = new InitiatorDirDialog();
                        break;
                    case "Items":
                        dirView = new ItemDirDialog();
                        break;
                    case "Provider":
                        dirView = new ProviderDirDialog();
                        break;
                    case "Significance":
                        dirView = new SignificanceDirDialog();
                        break;
                    case "Status":
                        dirView = new StatusDirDialog();
                        break;
                    default:
                        return false;
                }
                var dialog = DialogHost.Show(dirView, DialogId);
                return dialog.IsCompleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
                return false;
            }
        }

        public static async void ShowDataSource()
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new DataSourceDialog(), DialogId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }
        }

        public static async Task<Request?> ShowRequest(Request request)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (await DialogHost.Show(new RequestDialog(request), DialogId) is Request result) 
                    return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }

            return null;
        }

        public static async Task<Filter> ShowFilter(Filter filter)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (await DialogHost.Show(new FilterDialog(filter), DialogId) is Filter result)
                    return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }

            return new Filter();
        }

        public static async void ShowDocuments(long requestId)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new FilesDialog(requestId), DialogId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }
        }

        public static async void ShowInfo()
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new InfoDialog(), DialogId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CloseMessages();
            }
        }

        public static void CloseMessages(object param = null!) => DialogHost.CloseDialogCommand.Execute(param, null);
    }
}
