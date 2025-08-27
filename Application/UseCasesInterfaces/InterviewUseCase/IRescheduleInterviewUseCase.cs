using Application.DTOs.Interview;

namespace Application.UseCasesInterfaces.InterviewUseCase
{
    public interface IRescheduleInterviewUseCase
    {
        Task ExecuteAsync(RescheduleInterviewDTO dto);
    }
}