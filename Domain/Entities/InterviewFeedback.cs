using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InterviewFeedback : BaseEntity
    {
        [Required]
        public Guid InterviewId { get; set; }
        
        [Required]
        public Guid InterviewerId { get; set; }
        
        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }
        
        [Required]
        [StringLength(2000)]
        public string Notes { get; set; } = string.Empty;

        // Navigation
        public Interview Interview { get; set; } = null!;
        public User Interviewer { get; set; } = null!;
    }

}
