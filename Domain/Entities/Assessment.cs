using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Assessment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid VacancyId { get; set; }
        public string Title { get; set; }
        public int TotalDuration { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsActive { get; set; }

        // Navigation
        public Vacancy Vacancy { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }

}
