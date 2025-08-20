using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Application
{
    public class UpdateApplicationStatusDTO
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty; // "Pending", "Shortlisted", "Rejected", "Hired"
    }
}