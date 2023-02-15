using PropertyChanged;
using SupplyApp.Entity;
using SupplyApp.Model;
using SupplyApp.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupplyApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    internal class FilterViewModel
    {
        private readonly RequestModel _requestModel = new();

        public Filter Filter { get; set; }

        public ObservableCollection<Initiator> Initiators { get; set; }
        public ObservableCollection<Executor> Executors { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Significance> Significances { get; set; }
        public ObservableCollection<Status> Statuses { get; set; }

        public FilterViewModel(Filter filter)
        {
            Filter = filter;
            Initiators = _requestModel.GetInitiators();
            Executors = _requestModel.GetExecutors();
            Providers = _requestModel.GetProviders();
            Significances = _requestModel.GetSignificances();
            Statuses = _requestModel.GetStatuses();
        }

        public ICommand ResetCommand => new RelayCommand((obj) =>
        {
            Filter.Reset();
            DialogController.CloseMessages(Filter);
        });
    }
}
