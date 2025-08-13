using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InterviewFeedback : BaseEntity
    {
        public Guid InterviewId { get; set; }
        public Guid InterviewerId { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }

        // Navigation
        public Interview Interview { get; set; } = null!;
        public User Interviewer { get; set; } = null!;
    }

}
