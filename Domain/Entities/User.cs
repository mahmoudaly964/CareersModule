using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin or Candidate

        // Navigation
        public Candidate? Candidate { get; set; }
    }

}
