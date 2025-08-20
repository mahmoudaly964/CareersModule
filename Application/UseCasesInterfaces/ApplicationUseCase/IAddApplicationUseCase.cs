using Application.DTOs.Application;

namespace Application.UseCasesInterfaces.ApplicationUseCase
{
    public interface IAddApplicationUseCase
    {
        Task ExecuteAsync(AddApplicationDTO newApplicationDTO);
    }
}