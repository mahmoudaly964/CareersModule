using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Vacancy
{
    public class VacancyResponseDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string RequiredQualifications { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }

    }
}
