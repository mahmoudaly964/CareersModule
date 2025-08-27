using Application.DTOs.Assessment;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface ISubmitAnswerUseCase
    {
        Task ExecuteAsync(SubmitAnswerDTO submitAnswerDTO);
    }
}