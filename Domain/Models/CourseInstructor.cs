using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CourseInstructor
    {
        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

        public Guid InstructorId { get; set; }
        public Instructors Instructors { get; set; }
    }
}
