using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Application
{
    public class AddApplicationDTO
    {
        [Required]
        public Guid UserId { get; set; } // The user making the application

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

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string University { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string College { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Major { get; set; } = string.Empty;

        [Required]
        public DateTime GraduationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Degree { get; set; } = string.Empty;
    }
}