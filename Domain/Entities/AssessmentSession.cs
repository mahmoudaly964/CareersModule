using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentSession : BaseEntity
    {
        [Required]
        public Guid ApplicationId { get; set; }
        
        [Required]
        public Guid AssessmentId { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public bool IsSubmitted { get; set; }
        
        public DateTime ExpectedEndTime => StartTime.AddSeconds(Assessment.TotalDuration);

        // Navigation properties
        public Application Application { get; set; } = null!;
        public Assessment Assessment { get; set; } = null!;
        public ICollection<QuestionSession> QuestionSessions { get; set; } = new List<QuestionSession>();
        public ICollection<CandidateAnswer> Answers { get; set; } = new List<CandidateAnswer>();
    }
}
