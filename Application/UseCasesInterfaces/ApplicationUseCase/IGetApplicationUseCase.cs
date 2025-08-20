using Application.DTOs.Application;

namespace Application.UseCasesInterfaces.ApplicationUseCase
{
    public interface IGetApplicationUseCase
    {
        Task<ApplicationResponseDTO> ExecuteAsync(Guid applicationId);
    }
}