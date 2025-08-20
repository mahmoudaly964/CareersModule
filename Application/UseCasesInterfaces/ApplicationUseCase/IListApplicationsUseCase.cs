using Application.DTOs.Application;

namespace Application.UseCasesInterfaces.ApplicationUseCase
{
    public interface IListApplicationsUseCase
    {
        Task<IEnumerable<ApplicationResponseDTO>> ExecuteAsync(
            string? status, 
            Guid? vacancyId, 
            Guid? candidateId, 
            string? candidateName,
            string? vacancyTitle,
            int pageNumber, 
            int pageSize);
    }
}