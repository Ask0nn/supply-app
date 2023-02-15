using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SupplyApp.Entity
{
    [DataContract]
    internal class DefaultsModel
    {
        public List<string> Executor { get; set; }
        public List<string> Initiator { get; set; }
        public List<string> Item { get; set; }
        public List<string> Provider { get; set; }
        public List<string> Request { get; set; }
        public List<string> Significance { get; set; }
        public List<KeyValuePair<string, string>> Status { get; set; }

        public DefaultsModel(List<string> executor, List<string> initiator, List<string> item, List<string> provider, List<string> request, List<string> significance, List<KeyValuePair<string, string>> status)
        {
            this.Executor = executor;
            this.Initiator = initiator;
            this.Item = item;
            this.Provider = provider;
            this.Request = request;
            this.Significance = significance;
            this.Status = status;
        }

        public DefaultsModel()
        {
        }
    }
}
