namespace Application.UseCasesInterfaces.Vacancy
{
    public interface IDeleteVacancyUseCase
    {
        public Task ExecuteAsync(Guid vacancyId);
    }
}
