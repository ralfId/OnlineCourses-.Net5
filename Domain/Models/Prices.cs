using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Prices
    {
        [Key]
        public int PriceId { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Promotion { get; set; }
        public int CourseId { get; set; }
        public Courses Courses { get; set; }

    }
}
