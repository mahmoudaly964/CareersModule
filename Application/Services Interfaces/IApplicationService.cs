using Application.DTOs.Application;

namespace Application.Services_Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationResponseDTO>> GetAllApplicationsAsync(
            string? status, 
            Guid? vacancyId, 
            Guid? candidateId, 
            string? candidateName,
            string? vacancyTitle,
            int pageNumber, 
            int pageSize);
        Task<ApplicationResponseDTO> GetApplicationByIdAsync(Guid applicationId);
        Task CreateApplicationAsync(AddApplicationDTO applicationDTO);
        Task UpdateApplicationStatusAsync(Guid applicationId, UpdateApplicationStatusDTO statusDTO);
    }
}