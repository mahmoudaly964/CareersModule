using Application.DTOs.Interview;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.InterviewUseCase;

namespace Application.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IScheduleInterviewUseCase _scheduleInterviewUseCase;
        private readonly IRescheduleInterviewUseCase _rescheduleInterviewUseCase;
        private readonly ICancelInterviewUseCase _cancelInterviewUseCase;

        public InterviewService(
            IScheduleInterviewUseCase scheduleInterviewUseCase,
            IRescheduleInterviewUseCase rescheduleInterviewUseCase,
            ICancelInterviewUseCase cancelInterviewUseCase)
        {
            _scheduleInterviewUseCase = scheduleInterviewUseCase;
            _rescheduleInterviewUseCase = rescheduleInterviewUseCase;
            _cancelInterviewUseCase = cancelInterviewUseCase;
        }

        public Task<Guid> ScheduleInterviewAsync(ScheduleInterviewDTO dto)
            => _scheduleInterviewUseCase.ExecuteAsync(dto);

        public Task RescheduleInterviewAsync(RescheduleInterviewDTO dto)
            => _rescheduleInterviewUseCase.ExecuteAsync(dto);

        public Task CancelInterviewAsync(CancelInterviewDTO dto)
            => _cancelInterviewUseCase.ExecuteAsync(dto);
    }
}