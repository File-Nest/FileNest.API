using Microsoft.Extensions.Options;

namespace FileNest.Service.Configuration
{
    public class DatabaseConnectionProvider
    {
        private readonly DatabaseSettings _databaseSettings;
        public DatabaseConnectionProvider(IOptions<DatabaseSettings> options)
        {
            _databaseSettings = options.Value;
        }
        public DatabaseConnections GetDatabaseConfiguration(string databaseName)
        {
            if (!_databaseSettings.Databases.TryGetValue(databaseName,out var database))
            {
                throw new ArgumentException(
                    $"Database '{databaseName}' is not configured.");
            }
            return database;
        }
        public string GetConnectionString(string databaseName)
        {
            return GetDatabaseConfiguration(databaseName).ConnectionString;
        }
    }
}