using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.Assessment
{
    public class StartAssessmentDTO
    {
        [Required]
        public Guid AssessmentId { get; set; }

        [Required]
        public Guid ApplicationId { get; set; }
    }

    public class AssessmentSessionResponseDTO
    {
        public Guid Id { get; set; }
        public Guid AssessmentId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public bool IsSubmitted { get; set; }
        public string AssessmentTitle { get; set; } = string.Empty;
        public int TotalDuration { get; set; }
        public int RemainingTime { get; set; }
        public List<QuestionForCandidateDTO> Questions { get; set; } = new List<QuestionForCandidateDTO>();
    }

    public class QuestionForCandidateDTO
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public int TimeLimit { get; set; }
        public List<OptionForCandidateDTO>? Options { get; set; }
        public CandidateAnswerDTO? CurrentAnswer { get; set; }
    }

    public class OptionForCandidateDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        // IsCorrect is NOT included for candidates
    }

    public class CandidateAnswerDTO
    {
        public Guid QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public Guid? SelectedOptionId { get; set; }
    }

    public class SubmitAnswerDTO
    {
        [Required]
        public Guid AssessmentSessionId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        public string? AnswerText { get; set; }

        public Guid? SelectedOptionId { get; set; }
    }

    public class SubmitAssessmentDTO
    {
        [Required]
        public Guid AssessmentSessionId { get; set; }
    }
}