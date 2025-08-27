using Application.DTOs.Assessment;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface IStartQuestionUseCase
    {
        Task<QuestionResponseDTO> ExecuteAsync(StartQuestionDTO startQuestionDTO);
    }
}