using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ModelsDto
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public Guid ObjectReference { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public string Content { get; set; }
    }
}
