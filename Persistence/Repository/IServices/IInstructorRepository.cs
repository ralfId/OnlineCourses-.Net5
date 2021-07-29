using Persistence.DapperConfiguration.DapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.IServices
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<InstructorDM>> GetAllAsync();
        Task<InstructorDM> GetItemByIdAsync(Guid id);
        Task<int> CreateItemAsync(string name, string lastname, string degree);
        Task<int> UpdateItemAsync(InstructorDM instructor);
        Task<int> DeleteItemAsync(Guid id);
    }
}
