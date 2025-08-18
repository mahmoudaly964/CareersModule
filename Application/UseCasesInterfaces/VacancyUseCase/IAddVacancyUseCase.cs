using Application.DTOs.Vacancy;

namespace Application.UseCasesInterfaces.Vacancy
{
    public interface IAddVacancyUseCase
    {
        public Task<bool> ExcuteAsync(AddVacancyDTO newVacancy);
    }
}
