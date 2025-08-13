using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum QuestionType
    {
        MultipleChoice, 
        Written 
    }
    public class Question : BaseEntity
    {
        public Guid AssessmentId { get; set; }
        public string QuestionText { get; set; } = null!;
        public QuestionType Type { get; set; } 
        public int TimeLimit { get; set; } 

        public Assessment Assessment { get; set; } = null!;
        public ICollection<QuestionOption>? Options { get; set; } 
    }
}
