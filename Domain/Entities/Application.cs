using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Application : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public Guid VacancyId { get; set; }
        public string Status { get; set; } // "Pending", "Shortlisted", "Rejected", "Hired"
        string ResumeUrl { get; set; } 
        decimal ?ExpectedSalary { get; set; }

        // Navigation
        public Candidate Candidate { get; set; } = null!;
        public Vacancy Vacancy { get; set; } = null!;
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }

}
