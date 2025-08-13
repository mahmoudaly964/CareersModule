using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuestionOption : BaseEntity
    {
        [Required]
        public Guid QuestionId { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;
        
        public bool IsCorrect { get; set; }

        // Navigation
        public Question Question { get; set; } = null!;
    }
}
