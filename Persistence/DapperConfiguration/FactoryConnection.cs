using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<ConnectionConfiguration> _configs;
        public FactoryConnection(IDbConnection connection, IOptions<ConnectionConfiguration> configs)
        {
            _connection = connection;
            _configs = configs;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null) _connection = new SqlConnection(_configs.Value.SQLConnection);

            if (_connection.State != ConnectionState.Open) _connection.Open();

            return _connection;

        }
    }
}
