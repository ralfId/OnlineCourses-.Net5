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
        Task<IList<InstructorDM>> GetAllAsync();
        Task<InstructorDM> GetItemByIdAsync(Guid id);
        Task<int> CreateItemAsync(InstructorDM instructor);
        Task<int> UpdateItemAsync(InstructorDM instructor);
        Task<int> DeleteItemAsync(Guid id);
    }
}
