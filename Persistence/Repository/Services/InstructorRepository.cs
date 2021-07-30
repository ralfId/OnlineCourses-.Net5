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
            catch (Exception ex)
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

        public async Task<int> CreateItemAsync(string name, string lastname, string degree)
        {
            var sp = "sp_Create_Instructor";
            int result = 0;
            try
            {
                var connection = _factoryConn.GetConnection();
                result = await connection.ExecuteAsync(
                    sp,
                    new
                    {
                        InstructorId = Guid.NewGuid(),
                        Name = name,
                        LastName = lastname,
                        Degree = degree
                    },
                    commandType: CommandType.StoredProcedure
                    );

            }
            catch (Exception ex)
            {
                throw new Exception("Can´t create an instructor >>> " + ex.Message.ToString());
            }
            finally
            {
                _factoryConn.CloseConnection();
            }

            return result;
        }

        public async Task<int> UpdateItemAsync(Guid id, string name, string lastname, string degree)
        {
            var sp = "sp_Update_Instructor";
            int result = 0;

            try
            {
                var connection = _factoryConn.GetConnection();
                result = await connection.ExecuteAsync(
                    sp,
                    new
                    {
                        InstructorId = id,
                        Name = name,
                        Lastname = lastname,
                        Degree  = degree
                    },
                    commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception("Can´t update the Instructor >>>" + ex.Message.ToString());
            }
            finally
            {
                _factoryConn.CloseConnection();
            }

            return result;
        }

        public async Task<int> DeleteItemAsync(Guid id)
        {
            var sp = "sp_Delete_Instructor";
            int result = 0;

            try
            {
                var connetion = _factoryConn.GetConnection();

                result = await connetion.ExecuteAsync(sp, new { InstructorId = id }, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw new Exception("can't delete the course >>> " + ex.Message.ToString());
            }
            finally
            {
                _factoryConn.CloseConnection();
            }
            return result;
        }
    }
}
