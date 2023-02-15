using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertyChanged;

namespace SupplyApp.Entity;

[AddINotifyPropertyChangedInterface]
public class Filter
{
    public string Num { get; set; } = null!;
    public Initiator Initiator { get; set; } = null!;
    public Executor Executor { get; set; } = null!;
    public Provider Provider { get; set; } = null!;
    public Significance Significance { get; set; } = null!;
    public Status Status { get; set; } = null!;
    public long? StartDate { get; set; }
    public long? EndDate { get; set; }
    public long? StartExecutionDate { get; set; }
    public long? EndExecutionDate { get; set; }

    public bool IsFiltered { get; private set; }

    public void CheckFilters()
    {
        IsFiltered = Num != null || Initiator != null || Executor != null || Provider != null || Significance != null ||
                     Status != null || StartDate > 0 || EndDate > 0 || StartExecutionDate > 0 || EndExecutionDate > 0;
    }

    public ObservableCollection<Request> ApplyFilter(ObservableCollection<Request> requests)
    {
        var result = new List<Request>();
        if (Num != null!)
            result = requests.Where(p => p.Num.Contains(Num, StringComparison.OrdinalIgnoreCase)).ToList();

        if (Initiator != null!)
            result = requests.Where(p => p.Initiator.Id.Equals(Initiator.Id)).ToList();

        if (Executor != null!)
            result = requests.Where(p => p.Executor.Id.Equals(Executor.Id)).ToList();

        if (Significance != null!)
            result = requests.Where(p => p.Significance.Id.Equals(Significance.Id)).ToList();

        if (Provider != null!)
            result = requests.Where(p => p.Provider.Id.Equals(Provider.Id)).ToList();

        if (Status != null!)
            result = requests.Where(p => p.Status.Id.Equals(Status.Id)).ToList();

        if (StartDate > 0)
            result = requests.Where(p => p.Date >= StartDate).ToList();

        if (EndDate > 0)
            result = requests.Where(p => p.Date <= EndDate).ToList();

        if (StartExecutionDate > 0)
            result = requests.Where(p => p.DateExecution >= StartExecutionDate).ToList();

        if (EndExecutionDate > 0)
            result = requests.Where(p => p.DateExecution <= EndExecutionDate).ToList();

        return new ObservableCollectionListSource<Request>(result);
    }

    public void Reset()
    {
        Num = null!;
        Initiator = null!;
        Executor = null!;
        Provider = null!;
        Status = null!;
        Significance = null!;
        StartDate = 0;
        EndDate = 0;
        StartExecutionDate = 0;
        EndExecutionDate = 0;
        IsFiltered = false;
    }
}