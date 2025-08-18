using Application.DTOs.Vacancy;

namespace Application.UseCasesInterfaces.Vacancy
{
    public interface IAddVacancyUseCase
    {
        public Task ExecuteAsync(AddVacancyDTO newVacancy);
    }
}
