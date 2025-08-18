using Application.DTOs.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.VacancyUseCase
{
    public interface IListVacancyUseCase
    {
        Task<IEnumerable<VacancyResponseDTO>> ExecuteAsync(string? role, int pageNumber, int pageSize);
    }
}
