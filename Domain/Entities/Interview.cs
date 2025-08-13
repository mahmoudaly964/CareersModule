using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Interview: BaseEntity
    {
        public Guid ApplicationId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string MeetingLink { get; set; } = string.Empty;
        public string Status { get; set; } = "Scheduled";
        public DateTime? CompletedDate { get; set; }

        // Navigation
        public Application Application { get; set; } = null!;
        public ICollection<InterviewFeedback> Feedbacks { get; set; } = new List<InterviewFeedback>();
    }
}
