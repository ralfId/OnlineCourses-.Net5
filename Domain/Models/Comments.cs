using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comments
    {
        [Key]
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
        public Guid CourseId { get; set; }
        public Courses Course { get; set; }
    }
}
