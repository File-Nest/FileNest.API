using FileNest.API.Configuration;
using Microsoft.Extensions.Options;

namespace FileNest.API.Services
{
    public class DatabaseConnectionService
    {
        private readonly DatabaseSettings _settings;

        public DatabaseConnectionService(
            IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetConnectionString(string databaseName)
        {
            if (!_settings.Connections.TryGetValue(
                    databaseName,
                    out var connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string for '{databaseName}' was not found.");
            }

            return connectionString;
        }
    }
}
