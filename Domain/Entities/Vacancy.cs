using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Vacancy : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredQualifications { get; set; }
        public string Role { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsPublished { get; set; }

        // Navigation
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }

}
