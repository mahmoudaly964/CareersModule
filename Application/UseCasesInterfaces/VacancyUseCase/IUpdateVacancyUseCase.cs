using Application.DTOs.Vacancy;

namespace Application.UseCasesInterfaces.Vacancy
{
    public interface IUpdateVacancyUseCase
    {
        public Task ExecuteAsync(UpdateVacancyDTO updateVacancyDTO,Guid vacancyId);
    }
}
