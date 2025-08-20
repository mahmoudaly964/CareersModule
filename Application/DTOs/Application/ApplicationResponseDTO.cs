namespace Application.DTOs.Application
{
    public class ApplicationResponseDTO
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid VacancyId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ResumeUrl { get; set; } = string.Empty;
        public string? LinkedInUrl { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties
        public string? CandidateName { get; set; }
        public string? VacancyTitle { get; set; }
    }
}