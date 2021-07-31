using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelsDto
{
    public class InstructorDto
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Degree { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
