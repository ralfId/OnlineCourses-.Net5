using Persistence.Pagination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.IServices
{
    public interface IPaginationRepository
    {
        Task<PaginationModel> returnPageAsync(
            string StoreProcedure,
            int PageNumber,
            int CuantityElements,
            IDictionary<string, object> Filter,
            string OrderColumn);
    }
}
