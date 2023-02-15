using PropertyChanged;
using System.Collections.Generic;

namespace SupplyApp.Entity;

[AddINotifyPropertyChangedInterface]
public partial class Status
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Color { get; set; } = "#000000";

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
