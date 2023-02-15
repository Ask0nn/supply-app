using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyApp.Entity
{
    [AddINotifyPropertyChangedInterface]
    public partial class Document
    {
        public long Id { get; set; }
        public string Name { get; set; } = ""; 
        public string Path { get; set; } = "";
        public long RequestId { get; set; }

        public virtual Request Request { get; set; } = null!;
    }
}
