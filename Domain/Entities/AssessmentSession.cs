using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentSession : BaseEntity
    {
        public Guid ApplicationId { get; set; }
        public Guid AssessmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
        public bool IsSubmitted { get; set; }
        public DateTime ExpectedEndTime => StartTime.AddSeconds(Assessment.TotalDuration);

        // Navigation properties
        public Application Application { get; set; }
        public Assessment Assessment { get; set; }
        public ICollection<CandidateAnswer> Answers { get; set; } = new List<CandidateAnswer>();


    }
}
