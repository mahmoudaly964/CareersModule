using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CandidateAnswer : BaseEntity
    {
        public Guid AssessmentSessionId { get; set; }
        public Guid QuestionId { get; set; }
        public string? AnswerText { get; set; } // for written question
        public Guid? SelectedOptionId { get; set; } // for mcq questions
        public AssessmentSession AssessmentSession { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public QuestionOption? SelectedOption { get; set; } 
    }
}
