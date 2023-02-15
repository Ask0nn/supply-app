using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class DataSourceViewModel
    {
        private readonly DefaultDataLoad _dataLoad = DefaultDataLoad.GetInstance;

        public ObservableCollection<ConnectionModel> Connections { get; set; }
        public SnackbarMessageQueue SnackBarMessageQueue { get; set; } = new();

        public string EditBox { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public ConnectionModel SelectedItem { get; set; } = null!;

        public DataSourceViewModel()
        {
            Connections = new ObservableCollectionListSource<ConnectionModel>(_dataLoad.Connections!.Connections);
            Connections.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (ConnectionModel con in e.NewItems)
                            SnackBarMessageQueue.Enqueue(_dataLoad.AddCon(con)
                                ? "Запись добавлена"
                                : "Такая запись уже существует");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (ConnectionModel con in e.OldItems)
                            SnackBarMessageQueue.Enqueue(_dataLoad.RemoveCon(con)
                                ? "Запись удалена"
                                : "Данную запись нельзя удалить");
                    break;
            }
        }

        public ICommand AddCommand => new RelayCommand((obj) =>
        {
            if (string.IsNullOrEmpty(EditBox))
            {
                SnackBarMessageQueue.Enqueue("Заполните поле");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DB files (*.sqlite)|*.sqlite|All files (*.*)|*.*";
            openFileDialog.Title = "Выбор файла базы данных";
            if (openFileDialog.ShowDialog() == true)
            {
                Path = openFileDialog.FileName;
                if (string.IsNullOrEmpty(Path))
                {
                    SnackBarMessageQueue.Enqueue("Вы не выбрали файл базы данных");
                    return;
                }
            }
            Connections.Add(new ConnectionModel() { Name = EditBox, Path = Path});
            EditBox = string.Empty;

            Connections = new ObservableCollectionListSource<ConnectionModel>(_dataLoad.Connections!.Connections);
            Connections.CollectionChanged += CollectionChanged;
        });

        public ICommand RemoveCommand => new RelayCommand((obj) =>
        {
            Connections.Remove(SelectedItem);
            Connections = new ObservableCollectionListSource<ConnectionModel>(_dataLoad.Connections!.Connections);
            Connections.CollectionChanged += CollectionChanged;
        }, (obj) => SelectedItem != null);

        public ICommand SetConnectionCommand => new RelayCommand((obj) =>
        {
            if (!File.Exists(SelectedItem.Path))
            {
                SnackBarMessageQueue.Enqueue("Ошибка!\nДанный файл отсутствует на компьютере.");
                return;
            }
            if (!_dataLoad.SetCon(SelectedItem))
            {
                SnackBarMessageQueue.Enqueue("Ошибка подключения!");
                return;
            }
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }, (obj) => SelectedItem != null);
    }
}
