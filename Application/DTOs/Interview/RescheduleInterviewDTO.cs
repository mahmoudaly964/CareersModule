using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Interview
{
    public class RescheduleInterviewDTO
    {
        [Required]
        public Guid InterviewId { get; set; }

        [Required]
        public DateTime NewScheduledDate { get; set; }

        [Required]
        [StringLength(500)]
        [Url]
        public string NewMeetingLink { get; set; } = string.Empty;
    }
}