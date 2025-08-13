using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vacancy
{
    public class UpdateVacancyDTO
    {
        [StringLength(200)]
        public string? Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? RequiredQualifications { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Role { get; set; } = string.Empty;
        public DateTime?  Deadline { get; set; }

    }
}
