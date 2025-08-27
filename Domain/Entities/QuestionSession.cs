using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class QuestionSession : BaseEntity
    {
        [Required]
        public Guid AssessmentSessionId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        // Navigation
        public AssessmentSession AssessmentSession { get; set; } = null!;
        public Question Question { get; set; } = null!;
    }
}