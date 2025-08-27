using Application.DTOs.Question;
using Application.Responses;
using Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SuccessResponse<Guid>>> AddQuestion([FromBody] AddQuestionDTO addQuestionDTO)
        {
            var questionId = await _questionService.AddQuestionAsync(addQuestionDTO);
            var response = new SuccessResponse<Guid>(questionId, 201);
            return Created($"api/question/{questionId}", response);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuccessResponse<string>>> UpdateQuestion([FromBody] UpdateQuestionDTO updateQuestionDTO)
        {
            await _questionService.UpdateQuestionAsync(updateQuestionDTO);
            var response = new SuccessResponse<string>("Question updated successfully");
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuccessResponse<string>>> DeleteQuestion([FromBody] Guid id)
        {
            await _questionService.DeleteQuestionAsync(id);
            var response = new SuccessResponse<string>("Question deleted successfully");
            return Ok(response);
        }
    }
}