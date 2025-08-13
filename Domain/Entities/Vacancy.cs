using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Vacancy : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(1000)]
        public string RequiredQualifications { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Role { get; set; } = string.Empty;
        
        [Required]
        public DateTime Deadline { get; set; }
        
        public bool IsPublished { get; set; }

        // Navigation
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
