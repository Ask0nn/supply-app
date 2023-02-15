using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Model;
using SupplyApp.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Excel = Microsoft.Office.Interop.Excel;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class MainViewModel
    {
        private readonly MainModel _mainModel;

        public ObservableCollection<Request> Requests { get; set; }
        public Request SelectedItem { get; set; } = new();
        public DateTime DtInformation { get; set; }
        public string? DbPath => _mainModel.GetRoute()?.Replace("Data Source=", "");
        public Filter Filter { get; set; }

        public MainViewModel()
        {
            _mainModel = new MainModel();
            Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
            Filter = new Filter();

            DispatcherTimer timer = new();
            DtInformation = DateTime.Now;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            DtInformation = DateTime.Now;
        }

        public ICommand LoadDefaultDataCommand => new RelayCommand(async (obj) =>
        {
            bool result = await DialogController.ShowBoolean(
                "Вы уверены, что хотите добавить стандартные данные в БД ?");
            if (!result) return;
            DefaultDataLoad.GetInstance.LoadDirs();
            DialogController.ShowAlert("Данные добавлены");
        });

        public ICommand DirsCommand => new RelayCommand((obj) =>
        {
            if (string.IsNullOrEmpty(obj.ToString())) 
                return;
            DialogController.ShowDir(obj.ToString());
        });

        public ICommand RefreshCommand => new RelayCommand((obj) =>
        {
            Filter = new Filter();
            Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
        });

        public ICommand OpenDataSourceCommand => new RelayCommand((obj) =>
        {
            DialogController.ShowDataSource();
        });

        public ICommand OpenRequestCommand => new RelayCommand(async (obj) =>
        {
            var request = new Request();
            if (await DialogController.ShowRequest(request) == null) return;
            if (!_mainModel.Add(request))
                DialogController.ShowAlert("Произошла ошибка при изменении");
            else Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
        });

        public ICommand OpenEditRequestCommand => new RelayCommand(async (obj) =>
        {
            Request? request;
            if ((request = await DialogController.ShowRequest(SelectedItem)) == null) return;
            if (!_mainModel.Update(request))
                DialogController.ShowAlert("Произошла ошибка при изменении");
            else Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
        }, (obj) => SelectedItem != null! && SelectedItem.Id > 0);

        public ICommand RemoveRequestCommand => new RelayCommand(async (obj) =>
        {
            if (await DialogController.ShowBoolean("Вы уверены что хотите удалить эту заявку ?"))
                DialogController.ShowAlert(_mainModel.Remove(SelectedItem)
                    ? "Запись удалена"
                    : "Произошла ошибка при удалении");
            Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
        }, (obj) => SelectedItem != null! && SelectedItem.Id > 0);

        public ICommand ChangeThemeCommand => new RelayCommand((obj) =>
        {
            PaletteHelper paletteHelper = new();
            ITheme theme = paletteHelper.GetTheme();
            if (Properties.Settings.Default.IsDark)
            {
                Properties.Settings.Default.IsDark = false;
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                theme.SetBaseTheme(Theme.Dark);
                Properties.Settings.Default.IsDark = true;
            }
            Properties.Settings.Default.Save();
            paletteHelper.SetTheme(theme);
        });

        public ICommand ExportCommand => new RelayCommand(async(obj) =>
        {
            await Task.Run(() =>
            {
                var excelList = from r in Requests
                                select new
                                {
                                    r.Num,
                                    Initiator = r.Initiator.Name,
                                    Executor = r.Executor.Name,
                                    Provider = r.Provider.Name,
                                    Significance = r.Significance.Name,
                                    Status = r.Status.Name,
                                    Date = DateTimeOffset.FromUnixTimeSeconds(r.Date).DateTime.ToString("dd.MM.yyyy"),
                                    DateExecution = r.DateExecution != null ? 
                                        DateTimeOffset.FromUnixTimeSeconds((long)r.DateExecution).DateTime.ToString("dd.MM.yyyy") :
                                        r.DateExecution.ToString(),
                                    Items = from i in r.RequestItems
                                            select new
                                            {
                                                Item = i.Item.Name,
                                                i.Count
                                            }
                                };

                var excel = new Excel.Application();
                var excelWorkbook = excel.Workbooks.Add();
                var excelSheet = (Excel.Worksheet)excelWorkbook.ActiveSheet;
                excelSheet.Name = "Exported";

                excelSheet.Cells[1, 1] = "Номер заявки";
                excelSheet.Cells[1, 2] = "Инициатор";
                excelSheet.Cells[1, 3] = "Исполнитель";
                excelSheet.Cells[1, 4] = "Поставщик";
                excelSheet.Cells[1, 5] = "Значимость";
                excelSheet.Cells[1, 6] = "Статус";
                excelSheet.Cells[1, 7] = "Дата заявки";
                excelSheet.Cells[1, 8] = "Дата исполнения заявки";

                int row = 2;
                foreach (var request in excelList)
                {
                    excelSheet.Cells[row, 1] = request.Num;
                    excelSheet.Cells[row, 2] = request.Initiator;
                    excelSheet.Cells[row, 3] = request.Executor;
                    excelSheet.Cells[row, 4] = request.Provider;
                    excelSheet.Cells[row, 5] = request.Significance;
                    excelSheet.Cells[row, 6] = request.Status;
                    excelSheet.Cells[row, 7] = request.Date;
                    excelSheet.Cells[row, 8] = request.DateExecution;
                    foreach (var item in request.Items)
                    {
                        row++;
                        excelSheet.Cells[row, 9] = item.Item;
                        excelSheet.Cells[row, 10] = item.Count;
                    }
                    row++;
                }

                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var filePath = $"{folderPath}\\{DateTime.Now:dd.MM.yyyy HH.mm.ss}.xlsx";
                excelWorkbook.Close(true, filePath);

                App.MainDispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    DialogController.ShowAlert($"Экспорт данных завешен.\nФайл: {filePath}");
                });
            });
        });

        public ICommand ShowFilterCommand => new RelayCommand(async(obj) =>
        {
            Filter = await DialogController.ShowFilter(Filter);
            Filter.CheckFilters();
            if (Filter.IsFiltered)
            {
                Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
                Requests = Filter.ApplyFilter(Requests);
            }
            else
            {
                Requests = new ObservableCollectionListSource<Request>(_mainModel.GetData());
                Filter = new Filter();
            }

        });

        public ICommand OpenFilesCommand => new RelayCommand( (obj) =>
        {
            DialogController.ShowDocuments(SelectedItem.Id);
        }, (obj) => SelectedItem != null! && SelectedItem.Id > 0);
    }
}
