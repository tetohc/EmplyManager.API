using EmplyManager.Entities.Models;
using Microsoft.Extensions.Options;

namespace EmplyManager.BL.Configuration
{
    /// <summary>
    /// Provide the connection string from the configuration settings.
    /// </summary>
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly ConnectionStringModel _connectionStrings;

        public ConnectionStringProvider(IOptions<ConnectionStringModel> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        /// <summary>
        /// Returns the default connection string.
        /// </summary>
        /// <returns>Returns the SQL connection string</returns>
        public string GetConnectionString() => _connectionStrings.SQLConnection;
    }
}