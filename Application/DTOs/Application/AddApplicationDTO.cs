using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Application
{
    public class AddApplicationDTO
    {
        [Required]
        public Guid CandidateId { get; set; }

        [Required]
        public Guid VacancyId { get; set; }

        [Required]
        [StringLength(500)]
        [Url]
        public string ResumeUrl { get; set; } = string.Empty;

        [StringLength(500)]
        [Url]
        public string? LinkedInUrl { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "ExpectedSalary must be non-negative")]
        public decimal? ExpectedSalary { get; set; }
    }
}