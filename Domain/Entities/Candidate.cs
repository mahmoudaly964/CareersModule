using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Candidate : BaseEntity
    {
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string University { get; set; }
        public string College { get; set; }
        public string Major { get; set; }

        public DateTime GraduationDate { get; set; }
        public string Degree { get; set; }
        

        // Navigation
        public User User { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }

}
