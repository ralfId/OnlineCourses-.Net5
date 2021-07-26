using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Courses
    {
        [Key]
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte[] CoverPhoto { get; set; }
        public Prices Prices { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<CourseInstructor> courseInstructor { get; set; }
    }
}
