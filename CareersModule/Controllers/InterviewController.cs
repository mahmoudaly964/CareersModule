using Application.DTOs.Interview;
using Application.Responses;
using Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _interviewService;

        public InterviewController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        [HttpPost("schedule")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<SuccessResponse<Guid>>> ScheduleInterview([FromBody] ScheduleInterviewDTO dto)
        {
            var id = await _interviewService.ScheduleInterviewAsync(dto);
            return Created($"api/interview/{id}", new SuccessResponse<Guid>(id, 201));
        }

        [HttpPut("reschedule")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<SuccessResponse<string>>> RescheduleInterview([FromBody] RescheduleInterviewDTO dto)
        {
            await _interviewService.RescheduleInterviewAsync(dto);
            return Ok(new SuccessResponse<string>("Interview rescheduled successfully"));
        }

        [HttpPut("cancel")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<SuccessResponse<string>>> CancelInterview([FromBody] CancelInterviewDTO dto)
        {
            await _interviewService.CancelInterviewAsync(dto);
            return Ok(new SuccessResponse<string>("Interview canceled successfully"));
        }
    }
}