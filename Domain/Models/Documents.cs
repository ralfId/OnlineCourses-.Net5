using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Documents
    {
        [Key]
        public Guid DocumentId { get; set; }
        public Guid ObjectReference { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        public byte[] Content { get; set; }
        public DateTime CreationDate { get; set; }




    }
}