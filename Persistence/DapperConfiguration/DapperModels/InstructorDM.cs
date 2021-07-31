using System;

namespace Persistence.DapperConfiguration.DapperModels
{
    public class InstructorDM
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Degree { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
