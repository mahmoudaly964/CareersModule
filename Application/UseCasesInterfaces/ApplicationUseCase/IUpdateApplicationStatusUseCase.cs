using Application.DTOs.Application;

namespace Application.UseCasesInterfaces.ApplicationUseCase
{
    public interface IUpdateApplicationStatusUseCase
    {
        Task ExecuteAsync(Guid applicationId, UpdateApplicationStatusDTO statusDTO);
    }
}