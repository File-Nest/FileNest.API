

using MongoDB.Driver;

namespace FileNest.API.Configuration
{
    public class DatabaseSettings
    {
        public Dictionary<string, DatabaseConnections> Databases { get; set; } = new();
    }
}