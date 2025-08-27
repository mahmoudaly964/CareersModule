using Application.DTOs.Interview;

namespace Application.Services_Interfaces
{
    public interface IInterviewService
    {
        Task<Guid> ScheduleInterviewAsync(ScheduleInterviewDTO dto);
        Task RescheduleInterviewAsync(RescheduleInterviewDTO dto);
        Task CancelInterviewAsync(CancelInterviewDTO dto);
    }
}