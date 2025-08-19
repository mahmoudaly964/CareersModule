using Application.DTOs.Vacancy;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services_Interfaces
{
    public interface IVacancyService
    {
        Task<IEnumerable<VacancyResponseDTO>> GetAllVacanciesAsync(string? role, int pageNumber, int pageSize);
        Task<IEnumerable<VacancyResponseDTO>> GetAllPublishedVacanciesAsync(string? role, int pageNumber, int pageSize);
        Task<VacancyResponseDTO> GetVacancyByIdAsync(Guid vacancyId);
        Task CreateVacancyAsync(AddVacancyDTO vacancy);
        Task UpdateVacancyAsync(UpdateVacancyDTO updateVacancyDTO, Guid vacancyId);
        Task DeleteVacancyAsync(Guid vacancyId);
        Task PublishVacancyAsync(Guid vacancyId);
        Task UnPublishVacancyAsync(Guid vacancyId);
    }
}
