using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Interview : BaseEntity
    {
        [Required]
        public Guid ApplicationId { get; set; }
        
        [Required]
        public DateTime ScheduledDate { get; set; }
        
        [Required]
        [StringLength(500)]
        [Url]
        public string MeetingLink { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";
        
        public DateTime? CompletedDate { get; set; }

        // Navigation
        public Application Application { get; set; } = null!;
        public ICollection<InterviewFeedback> Feedbacks { get; set; } = new List<InterviewFeedback>();
    }
}
