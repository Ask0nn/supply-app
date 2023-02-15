using System.Runtime.Serialization;

namespace SupplyApp.Entity
{
    [DataContract]
    internal class ConnectionModel
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ConnectionModel(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public ConnectionModel()
        {
            
        }
    }
}
