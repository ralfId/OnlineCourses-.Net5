using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Instructors
    {
        [Key]
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Degree { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public ICollection<Courses> CoursesList { get; set; }
    }
}
