using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Esercitazione.Services
{
    public class SqlServerServiceBase : ServiceBase
    {
        private SqlConnection _connection;
        public SqlServerServiceBase(IConfiguration config)
        {
            _connection = new SqlConnection(config.GetConnectionString("DbBW"));
        }
        protected override DbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _connection);
        }

        protected override DbConnection GetConnection()
        {
            return _connection;
        }
    }
}
