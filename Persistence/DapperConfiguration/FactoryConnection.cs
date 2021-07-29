using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DapperConfiguration
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;
        public FactoryConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
