using MongoDB.Driver;
using System.Runtime;

namespace FileNest.Service.Configuration
{
    public class DatabaseConnections
    {
        public string Provider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
