using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CandidateAnswer : BaseEntity
    {
        [Required]
        public Guid AssessmentSessionId { get; set; }
        
        [Required]
        public Guid QuestionId { get; set; }
        
        [StringLength(2000)]
        public string? AnswerText { get; set; } // for written questions
        
        public Guid? SelectedOptionId { get; set; } // for MCQ questions

        // Navigation
        public AssessmentSession AssessmentSession { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public QuestionOption? SelectedOption { get; set; }
    }
}
