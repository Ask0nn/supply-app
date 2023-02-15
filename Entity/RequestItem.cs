using PropertyChanged;

namespace SupplyApp.Entity;

[AddINotifyPropertyChangedInterface]
public partial class RequestItem
{
    public long Id { get; set; }

    public long ItemId { get; set; }

    public long RequestId { get; set; }

    public long Count { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Request Request { get; set; } = null!;
}
