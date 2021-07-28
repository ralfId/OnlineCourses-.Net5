using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelsDto
{
    public  class PriceDto
    {
        public Guid PriceId { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Promotion { get; set; }
        public Guid CourseId { get; set; }
    }
}
