using System.Data;

namespace Persistence.DapperConfiguration
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}
