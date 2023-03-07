using Dapper;
using Persistence.DapperConfiguration;
using Persistence.Pagination.Models;
using Persistence.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.Services
{
    public class PaginationRepository : IPaginationRepository
    {
        private readonly IFactoryConnection _factoryConn;

        public PaginationRepository(IFactoryConnection factoryConn)
        {
            _factoryConn = factoryConn;
        }
        public async Task<PaginationModel> returnPageAsync(string StoreProcedure, int PageNumber, int CuantityElements, IDictionary<string, object> Filter, string OrderColumn)
        {
            List<IDictionary<string, object>> objectsList = null;
            PaginationModel pagination = new PaginationModel();
            int totalRecords = 0;
            int totalPages = 0;
            try
            {
                var connection = _factoryConn.GetConnection();

                //creation of parameters
                DynamicParameters parameters = new DynamicParameters();

                //input parameters
                foreach (var item in Filter)
                {
                    parameters.Add("@" + item.Key, item.Value);
                }
                parameters.Add("@NumeroPagina", PageNumber);
                parameters.Add("@CantidadElementos", CuantityElements);
                parameters.Add("@Ordenamiento", OrderColumn);
                //output parameters
                parameters.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TotalPaginas", totalPages, DbType.Int32, ParameterDirection.Output);



                //get list of results
                var result = await connection.QueryAsync(StoreProcedure, parameters, commandType: CommandType.StoredProcedure);
                //covert result to dictionary
                objectsList = result.Select(x => (IDictionary<string, object>)x).ToList();
                


                /* Create ouput model*/
                pagination.RecordList = objectsList;
                //get output parameters
                pagination.PageNumbers = parameters.Get<int>("@TotalPaginas");
                pagination.TotalRecords = parameters.Get<int>("@TotalRecords");
                /* End to create ouput model*/

            }
            catch (Exception ex)
            {
                throw new Exception("Can execute operation >>> " + ex.Message.ToString());
            }
            finally
            {
                _factoryConn.CloseConnection();
            }

            return pagination;
        }
    }
}
