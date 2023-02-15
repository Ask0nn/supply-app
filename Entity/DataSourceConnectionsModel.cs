using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SupplyApp.Entity
{
    [DataContract]
    internal class DataSourceConnectionsModel
    {
        public ConnectionModel? Active { get; set; }
        public List<ConnectionModel> Connections { get; set; }

        public DataSourceConnectionsModel()
        {
            Active = new ConnectionModel();
            Connections = new List<ConnectionModel>();
        }

        public DataSourceConnectionsModel(ConnectionModel? active, List<ConnectionModel> connections)
        {
            Active = active;
            Connections = connections;
        }
    }
}
