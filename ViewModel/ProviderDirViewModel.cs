using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Model;
using SupplyApp.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class ProviderDirViewModel
    {
        private readonly ProviderDirModel _providerDirModel = new();

        public ObservableCollection<Provider> Data { get; set; }
        public SnackbarMessageQueue SnackBarMessageQueue { get; set; }

        public Visibility AddBlockVisibility { get; set; }
        public Visibility UpdateBlockVisibility { get; set; }

        public string EditBox { get; set; }
        public Provider SelectedItem { get; set; } = null!;

        public ProviderDirViewModel()
        {
            AddBlockVisibility = Visibility.Collapsed;
            UpdateBlockVisibility = Visibility.Collapsed;

            SnackBarMessageQueue = new();
            EditBox = "";

            Data = new ObservableCollectionListSource<Provider>(_providerDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (Provider exec in e.NewItems)
                            SnackBarMessageQueue.Enqueue(_providerDirModel.Add(exec)
                                ? "Запись добавлена"
                                : "Такая запись уже существует");
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldItems != null)
                        foreach (Provider exec in e.OldItems)
                            SnackBarMessageQueue.Enqueue(_providerDirModel.Update(exec)
                                ? "Запись изменена"
                                : "Такая запись уже существует");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (Provider exec in e.OldItems)
                            SnackBarMessageQueue.Enqueue(_providerDirModel.Remove(exec)
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
            if (SelectedItem == null)
            {
                SnackBarMessageQueue.Enqueue("Выберите строку для изменения");
                return;
            }
            AddBlockVisibility = Visibility.Collapsed;
            UpdateBlockVisibility = UpdateBlockVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            EditBox = SelectedItem.Name;
        });

        public ICommand AddCommand => new RelayCommand((obj) =>
        {
            if (string.IsNullOrEmpty(EditBox))
            {
                SnackBarMessageQueue.Enqueue("Заполните поле");
                return;
            }
            Data.Add(new Provider() { Name = EditBox });

            Data = new ObservableCollectionListSource<Provider>(_providerDirModel.GetAll());
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
            Data[index] = SelectedItem;

            Data = new ObservableCollectionListSource<Provider>(_providerDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
            UpdateBlockVisibility = Visibility.Collapsed;
        });

        public ICommand RemoveCommand => new RelayCommand((obj) =>
        {
            if (SelectedItem == null)
            {
                SnackBarMessageQueue.Enqueue("Выберите строку для удаления");
                return;
            }
            Data.Remove(SelectedItem);
            Data = new ObservableCollectionListSource<Provider>(_providerDirModel.GetAll());
            Data.CollectionChanged += CollectionChanged;
        });
    }
}
