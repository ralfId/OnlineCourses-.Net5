using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Pagination.Models
{
    public class PaginationModel
    {
        public List<IDictionary<string, object>> RecordList { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumbers { get; set; }

    }
}
