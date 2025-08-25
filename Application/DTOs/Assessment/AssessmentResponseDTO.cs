using Domain.Entities;

namespace Application.DTOs.Assessment
{
    public class AssessmentResponseDTO
    {
        public Guid Id { get; set; }
        public Guid VacancyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalDuration { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? VacancyTitle { get; set; }
        public int QuestionCount { get; set; }
    }

    public class AssessmentDetailDTO
    {
        public Guid Id { get; set; }
        public Guid VacancyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalDuration { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? VacancyTitle { get; set; }
        public List<QuestionResponseDTO> Questions { get; set; } = new List<QuestionResponseDTO>();
    }

    public class QuestionResponseDTO
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public int TimeLimit { get; set; }
        public List<QuestionOptionResponseDTO>? Options { get; set; }
    }

    public class QuestionOptionResponseDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } // Only visible to admins
    }
}