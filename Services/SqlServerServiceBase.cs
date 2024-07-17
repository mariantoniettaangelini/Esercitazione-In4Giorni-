using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Esercitazione.Services
{
    public class SqlServerServiceBase : ServiceBase
    {
        private readonly IConfiguration _configuration;

        public SqlServerServiceBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override DbConnection CreateConnection()
        {
            // Crea una nuova connessione ogni volta che serve
            return new SqlConnection(_configuration.GetConnectionString("DbBW"));
        }

        protected override DbCommand GetCommand(string commandText, DbConnection connection)
        {
            // Assicura che il comando sia associato a una connessione aperta
            return new SqlCommand(commandText, connection as SqlConnection);
        }
    }
}
