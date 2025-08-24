using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Candidate : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string University { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string College { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Major { get; set; } = string.Empty;

        [Required]
        public DateTime GraduationDate { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Degree { get; set; } = string.Empty;

        // Navigation
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }

}
