using Application.DTOs.InterviewFeedback;

namespace Application.UseCasesInterfaces.InterviewFeedback
{
    public interface IAddInterviewFeedbackUseCase
    {
        Task<Guid> ExecuteAsync(AddInterviewFeedbackDTO dto);
    }
}