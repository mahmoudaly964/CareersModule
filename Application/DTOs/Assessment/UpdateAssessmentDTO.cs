using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Assessment
{
    public class UpdateAssessmentDTO
    {
        [Required]
        public Guid AssessmentId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(60, int.MaxValue, ErrorMessage = "Duration must be more than 1 minute")]
        public int TotalDuration { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public bool IsActive { get; set; }
    }
}