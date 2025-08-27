using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Assessment
{
    public class StartQuestionDTO
    {
        [Required]
        public Guid AssessmentSessionId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }
    }
}