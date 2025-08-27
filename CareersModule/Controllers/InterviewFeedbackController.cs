using Application.DTOs.InterviewFeedback;
using Application.Responses;
using Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewFeedbackController : ControllerBase
    {
        private readonly IInterviewFeedbackService _interviewFeedbackService;

        public InterviewFeedbackController(IInterviewFeedbackService interviewFeedbackService)
        {
            _interviewFeedbackService = interviewFeedbackService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SuccessResponse<Guid>>> AddFeedback([FromBody] AddInterviewFeedbackDTO dto)
        {
            var feedbackId = await _interviewFeedbackService.AddFeedbackAsync(dto);
            var response = new SuccessResponse<Guid>(feedbackId, 201);
            return Created($"api/interview-feedback/{feedbackId}", response);
        }
    }
}