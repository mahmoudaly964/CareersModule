using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Application : BaseEntity
    {
        [Required]
        public Guid CandidateId { get; set; }

        [Required]
        public Guid VacancyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // "Pending", "Shortlisted", "Rejected", "Hired"

        [Required]
        [StringLength(500)]
        [Url]
        public string ResumeUrl { get; set; } = string.Empty;
        [StringLength(500)]
        [Url]
        public string? LinkedInUrl { get; set; } = string.Empty;

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "ExpectedSalary must be non-negative")]
        public decimal? ExpectedSalary { get; set; }

        // Navigation
        public Candidate Candidate { get; set; } = null!;
        public Vacancy Vacancy { get; set; } = null!;
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }
}