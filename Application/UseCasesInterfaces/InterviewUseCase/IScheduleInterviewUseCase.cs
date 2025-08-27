using Application.DTOs.Interview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.InterviewUseCase
{
    public interface IScheduleInterviewUseCase
    {
        public Task<Guid> ExecuteAsync(ScheduleInterviewDTO dto);
    }
}
