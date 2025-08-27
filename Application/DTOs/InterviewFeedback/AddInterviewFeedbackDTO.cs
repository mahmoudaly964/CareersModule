using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.InterviewFeedback
{
    public class AddInterviewFeedbackDTO
    {
        public Guid InterviewId { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
