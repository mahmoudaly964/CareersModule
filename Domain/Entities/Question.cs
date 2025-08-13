using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Guid AssessmentId { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;
        
        [Required]
        public QuestionType Type { get; set; }
        
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="time limit of the question must be positive")] 
        public int TimeLimit { get; set; }

        // Navigation
        public Assessment Assessment { get; set; } = null!;
        public ICollection<QuestionOption>? Options { get; set; }
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; } = new List<CandidateAnswer>();
    }
}
