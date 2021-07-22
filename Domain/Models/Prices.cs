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
        public Guid PriceId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CurrentPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Promotion { get; set; }
        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

    }
}
