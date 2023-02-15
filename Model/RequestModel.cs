using System.Collections.Generic;
using SupplyApp.Entity;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SupplyApp.Model
{
    internal class RequestModel
    {
        private readonly ApplicationContext _context = new();

        public RequestModel()
        {
            _context.Executors.Load();
            _context.Initiators.Load();
            _context.Providers.Load();
            _context.Significances.Load();
            _context.Statuses.Load();
            _context.Items.Load();
        }

        public ObservableCollection<Executor> GetExecutors() => _context.Executors.Local.ToObservableCollection();
        public ObservableCollection<Initiator> GetInitiators() => _context.Initiators.Local.ToObservableCollection();
        public ObservableCollection<Provider> GetProviders() => _context.Providers.Local.ToObservableCollection();
        public ObservableCollection<Significance> GetSignificances() => _context.Significances.Local.ToObservableCollection();
        public ObservableCollection<Status> GetStatuses() => _context.Statuses.Local.ToObservableCollection();
        public ObservableCollection<Item> GetItems() =>  _context.Items.Local.ToObservableCollection();

        public ObservableCollection<Item> GetItems(ICollection<RequestItem> items) => new ObservableCollectionListSource<Item>(
            _context.Items.Local.Where(t => !items.Any(p => p.Item.Id.Equals(t.Id))));
    }
}
