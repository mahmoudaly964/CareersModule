using Application.DTOs.Assessment;
using Application.Responses;
using Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<Guid>>> CreateAssessment([FromBody] CreateAssessmentDTO createAssessmentDTO)
        {
            var assessmentId = await _assessmentService.CreateAssessmentAsync(createAssessmentDTO);
            var response = new SuccessResponse<Guid>(assessmentId, 201);
            return Created($"api/assessment/{assessmentId}", response);
        }

        [HttpGet("{assessmentId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<AssessmentResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<AssessmentResponseDTO>>> GetAssessment(Guid assessmentId)
        {
            var assessment = await _assessmentService.GetAssessmentAsync(assessmentId);
            var response = new SuccessResponse<AssessmentResponseDTO>(assessment);
            return Ok(response);
        }        

        [HttpPost("start")]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<AssessmentSessionResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<AssessmentSessionResponseDTO>>> StartAssessment([FromBody] StartAssessmentDTO startAssessmentDTO)
        {
            var session = await _assessmentService.StartAssessmentAsync(startAssessmentDTO);
            var response = new SuccessResponse<AssessmentSessionResponseDTO>(session);
            return Ok(response);
        }

        [HttpPost("start-question")]
        [Authorize]
        public async Task<ActionResult<SuccessResponse<QuestionResponseDTO>>> StartQuestion([FromBody] StartQuestionDTO dto)
        {
            var question=await _assessmentService.StartQuestionAsync(dto);
            var response = new SuccessResponse<QuestionResponseDTO>(question);
            return Ok(response);
        }

        [HttpPost("submit-answer")]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> SubmitAnswer([FromBody] SubmitAnswerDTO submitAnswerDTO)
        {
            await _assessmentService.SubmitAnswerAsync(submitAnswerDTO);
            var response = new SuccessResponse<string>("Answer submitted successfully");
            return Ok(response);
        }

        [HttpPost("submit-assesment")]
        [Authorize]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> SubmitAssessment([FromBody] SubmitAssessmentDTO submitAssessmentDTO)
        {
            await _assessmentService.SubmitAssessmentAsync(submitAssessmentDTO);
            var response = new SuccessResponse<string>("Assessment submitted successfully");
            return Ok(response);
        }

       
    }
}