using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelsDto
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte[] CoverPhoto { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<InstructorDto> Instructors { get; set; }
        public PriceDto Prices { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
