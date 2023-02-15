using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Model;
using SupplyApp.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class StatusDirViewModel
    {
        private readonly StatusDirModel _statusDirModel = new();

        public ObservableCollection<Status> Data { get; set; }
        public SnackbarMessageQueue SnackBarMessageQueue { get; set; }

        public Visibility AddBlockVisibility { get; set; }
        public Visibility UpdateBlockVisibility { get; set; }

        public string EditBox { get; set; }
        public string Color { get; set; } = "#32a852";
        public Status SelectedItem { get; set; } = null!;

        public StatusDirViewModel()
        {
            AddBlockVisibility = Visibility.Collapsed;
            UpdateBlockVisibility = Visibility.Collapsed;

            SnackBarMessageQueue = new();
            EditBox = "";

            Data = new ObservableCollectionListSource<Status>(_statusDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (Status exec in e.NewItems)
                            SnackBarMessageQueue.Enqueue(_statusDirModel.Add(exec)
                                ? "Запись добавлена"
                                : "Такая запись уже существует");
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems != null)
                        foreach (Status exec in e.OldItems)
                            SnackBarMessageQueue.Enqueue(_statusDirModel.Update(exec)
                                ? "Запись изменена"
                                : "Такая запись уже существует");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (Status exec in e.OldItems)
                            SnackBarMessageQueue.Enqueue(_statusDirModel.Remove(exec)
                                ? "Запись удалена"
                                : "Данную запись нельзя удалить");
                    break;
            }
        }

        public ICommand ShowAddCommand => new RelayCommand((obj) =>
        {
            UpdateBlockVisibility = Visibility.Collapsed;
            AddBlockVisibility = AddBlockVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            EditBox = string.Empty;
        });

        public ICommand ShowUpdateCommand => new RelayCommand((obj) =>
        {
            if (SelectedItem == null!)
            {
                SnackBarMessageQueue.Enqueue("Выберите строку для изменения");
                return;
            }
            AddBlockVisibility = Visibility.Collapsed;
            UpdateBlockVisibility = UpdateBlockVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            EditBox = SelectedItem.Name;
            Color = SelectedItem.Color!;
        });

        public ICommand AddCommand => new RelayCommand((obj) =>
        {
            if (string.IsNullOrEmpty(EditBox))
            {
                SnackBarMessageQueue.Enqueue("Заполните поле");
                return;
            }
            Data.Add(new Status() { Name = EditBox, Color = Color });

            Data = new ObservableCollectionListSource<Status>(_statusDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
            AddBlockVisibility = Visibility.Collapsed;
        });

        public ICommand UpdateCommand => new RelayCommand((obj) =>
        {
            if (string.IsNullOrEmpty(EditBox))
            {
                SnackBarMessageQueue.Enqueue("Заполните поле");
                return;
            }
            var index = Data.IndexOf(SelectedItem);
            SelectedItem.Name = EditBox;
            SelectedItem.Color = Color;
            Data[index] = SelectedItem;

            Data = new ObservableCollectionListSource<Status>(_statusDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
            UpdateBlockVisibility = Visibility.Collapsed;
        });

        public ICommand RemoveCommand => new RelayCommand((obj) =>
        {
            if (SelectedItem == null!)
            {
                SnackBarMessageQueue.Enqueue("Выберите строку для удаления");
                return;
            }
            Data.Remove(SelectedItem);
            Data = new ObservableCollectionListSource<Status>(_statusDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
        });

        public ICommand SelectColor => new RelayCommand((obj) =>
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Color = "#" + (colorDlg.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
            }
        });
    }
}
