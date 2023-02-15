using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Model;
using SupplyApp.Utils;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class RequestViewModel
    {
        private readonly RequestModel _requestModel = new();

        public Request Request { get; set; }

        public ObservableCollection<Initiator> Initiators { get; set; }
        public ObservableCollection<Executor> Executors { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Significance> Significances { get; set; }
        public ObservableCollection<Status> Statuses { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<RequestItem> RequestItems { get; set; }

        public Item SelectedItem { get; set; }
        public string ItemName { get; set; }
        public RequestItem SelectedRequestItem { get; set; }
        public int Count { get; set; }

        public RequestViewModel(Request request)
        {
            Request = request;
            RequestItems = new ObservableCollectionListSource<RequestItem>(request.RequestItems);
            Initiators = _requestModel.GetInitiators();
            Executors = _requestModel.GetExecutors();
            Providers = _requestModel.GetProviders();
            Significances = _requestModel.GetSignificances();
            Statuses = _requestModel.GetStatuses();
            Items = _requestModel.GetItems(RequestItems);
        }

        public ICommand AddItemCommand => new RelayCommand((obj) =>
        {
            RequestItem item = new()
            {
                Item = SelectedItem != null! ? SelectedItem : new Item() { Name = ItemName },
                Count = Count
            };
            Request.RequestItems.Add(item);
            RequestItems.Add(item);
            if (SelectedItem != null)
                Items.Remove(SelectedItem);
            Count = 0;
        }, (obj) => !string.IsNullOrEmpty(ItemName) && Count > 0);

        public ICommand RemoveItemCommand => new RelayCommand((obj) =>
        {
            Items.Add(SelectedRequestItem.Item);
            Request.RequestItems.Remove(SelectedRequestItem);
            RequestItems.Remove(SelectedRequestItem);
        }, (obj) => SelectedRequestItem != null!);
    }
}
