using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Assessment : BaseEntity
    {   
        [Required]
        public Guid VacancyId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [Range(60, int.MaxValue,ErrorMessage ="Duration must be more than 1 minute")]
        public int TotalDuration { get; set; }
        
        [Required]
        public DateTime Deadline { get; set; }
        
        public bool IsActive { get; set; }

        // Navigation
        public Vacancy Vacancy { get; set; } = null!;
        public ICollection<AssessmentSession> AssessmentSessions { get; set; } = new List<AssessmentSession>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
