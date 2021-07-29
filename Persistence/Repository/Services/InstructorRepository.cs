using Dapper;
using Persistence.DapperConfiguration;
using Persistence.DapperConfiguration.DapperModels;
using Persistence.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository.Services
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly IFactoryConnection _factoryConn;
        public InstructorRepository(IFactoryConnection factoryConn)
        {
            _factoryConn = factoryConn;
        }

        public async Task<IEnumerable<InstructorDM>> GetAllAsync()
        {
            IEnumerable<InstructorDM> instructorList = null;

            try
            {
                var connection = _factoryConn.GetConnection();
                var storeProcedure = "sp_Get_All_Instructors";
                instructorList = await connection.QueryAsync<InstructorDM>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _factoryConn.CloseConnection();
            }

            return instructorList;
        }

        public Task<InstructorDM> GetItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateItemAsync(InstructorDM instructor)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateItemAsync(InstructorDM instructor)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
