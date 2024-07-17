using System.Data.Common;

namespace Esercitazione.Services
{
    public abstract class ServiceBase
    {
        protected abstract DbConnection CreateConnection();
        protected abstract DbCommand GetCommand(string commandText, DbConnection connection);
    }
}
