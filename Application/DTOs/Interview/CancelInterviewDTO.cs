using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Interview
{
    public class CancelInterviewDTO
    {
        [Required]
        public Guid InterviewId { get; set; }
    }
}