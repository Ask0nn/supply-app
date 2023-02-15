using PropertyChanged;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SupplyApp.Entity;

[AddINotifyPropertyChangedInterface]
public partial class Request
{
    public long Id { get; set; }
    public string Num { get; set; } = "";
    public long SignificanceId { get; set; }
    public long StatusId { get; set; }
    public long Date { get; set; } = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
    public long ProviderId { get; set; }
    public long InitiatorId { get; set; }
    public long ExecutorId { get; set; }
    public long? DateExecution { get; set; }
    public string Description { get; set; } = "";

    public virtual Executor Executor { get; set; } = new();
    public virtual Initiator Initiator { get; set; } = new();
    public virtual Provider Provider { get; set; } = new();
    public virtual Significance Significance { get; set; } = new();
    public virtual Status Status { get; set; } = new();

    public virtual ICollection<RequestItem> RequestItems { get; } = new List<RequestItem>(); 
    public virtual ICollection<Document> Documents { get; } = new List<Document>();
}
