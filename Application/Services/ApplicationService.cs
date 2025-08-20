using Application.DTOs.Application;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.ApplicationUseCase;

namespace Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IAddApplicationUseCase _addApplicationUseCase;
        private readonly IGetApplicationUseCase _getApplicationUseCase;
        private readonly IListApplicationsUseCase _listApplicationsUseCase;
        private readonly IUpdateApplicationStatusUseCase _updateApplicationStatusUseCase;

        public ApplicationService(
            IAddApplicationUseCase addApplicationUseCase,
            IGetApplicationUseCase getApplicationUseCase,
            IListApplicationsUseCase listApplicationsUseCase,
            IUpdateApplicationStatusUseCase updateApplicationStatusUseCase)
        {
            _addApplicationUseCase = addApplicationUseCase;
            _getApplicationUseCase = getApplicationUseCase;
            _listApplicationsUseCase = listApplicationsUseCase;
            _updateApplicationStatusUseCase = updateApplicationStatusUseCase;
        }

        public async Task<IEnumerable<ApplicationResponseDTO>> GetAllApplicationsAsync(
            string? status, 
            Guid? vacancyId, 
            Guid? candidateId, 
            string? candidateName,
            string? vacancyTitle,
            int pageNumber, 
            int pageSize)
        {
            return await _listApplicationsUseCase.ExecuteAsync(status, vacancyId, candidateId, candidateName, vacancyTitle, pageNumber, pageSize);
        }

        public async Task<ApplicationResponseDTO> GetApplicationByIdAsync(Guid applicationId)
        {
            return await _getApplicationUseCase.ExecuteAsync(applicationId);
        }

        public async Task CreateApplicationAsync(AddApplicationDTO applicationDTO)
        {
            await _addApplicationUseCase.ExecuteAsync(applicationDTO);
        }

        public async Task UpdateApplicationStatusAsync(Guid applicationId, UpdateApplicationStatusDTO statusDTO)
        {
            await _updateApplicationStatusUseCase.ExecuteAsync(applicationId, statusDTO);
        }
    }
}