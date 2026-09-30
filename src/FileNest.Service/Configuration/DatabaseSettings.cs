using MongoDB.Driver;

namespace FileNest.Service.Configuration
{
    public class DatabaseSettings
    {
        public Dictionary<string, DatabaseConnections> Databases { get; set; } = new();
    }
}