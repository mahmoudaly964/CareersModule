using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Assessment
{
    public class CreateAssessmentDTO
    {
        [Required]
        public Guid VacancyId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(60, int.MaxValue, ErrorMessage = "Duration must be more than 1 minute")]
        public int TotalDuration { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public List<CreateQuestionDTO> Questions { get; set; } = new List<CreateQuestionDTO>();
    }

    public class CreateQuestionDTO
    {
        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Time limit must be positive")]
        public int TimeLimit { get; set; }

        public List<CreateQuestionOptionDTO>? Options { get; set; }
    }

    public class CreateQuestionOptionDTO
    {
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}