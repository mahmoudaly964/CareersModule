using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Interview
{
    public class ScheduleInterviewDTO
    {
        [Required]
        public Guid ApplicationId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        [StringLength(500)]
        [Url]
        public string MeetingLink { get; set; } = string.Empty;
    }
}