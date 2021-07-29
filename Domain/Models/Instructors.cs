using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public ICollection<CourseInstructor> CourseInstructor { get; set; }
    }
}
