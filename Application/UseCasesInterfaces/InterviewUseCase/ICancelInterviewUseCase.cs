using Application.DTOs.Interview;

namespace Application.UseCasesInterfaces.InterviewUseCase
{
    public interface ICancelInterviewUseCase
    {
        Task ExecuteAsync(CancelInterviewDTO dto);
    }
}