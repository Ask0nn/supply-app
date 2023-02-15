using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PropertyChanged;

namespace SupplyApp.Entity;

[AddINotifyPropertyChangedInterface]
public partial class Item
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RequestItem> RequestItems { get; } = new List<RequestItem>();
}
