using Application.DTOs.Application;
using Application.Responses;
using Application.Services_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("{applicationId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<ApplicationResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<ApplicationResponseDTO>>> GetApplicationById(Guid applicationId)
        {
            var application = await _applicationService.GetApplicationByIdAsync(applicationId);
            var response = new SuccessResponse<ApplicationResponseDTO>(application);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ApplicationResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<IEnumerable<ApplicationResponseDTO>>>> GetApplications(
            [FromQuery] string? status,
            [FromQuery] Guid? vacancyId,
            [FromQuery] Guid? candidateId,
            [FromQuery] string? candidateName,
            [FromQuery] string? vacancyTitle,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var applications = await _applicationService.GetAllApplicationsAsync(
                status, vacancyId, candidateId, candidateName, vacancyTitle, pageNumber, pageSize);
            var response = new SuccessResponse<IEnumerable<ApplicationResponseDTO>>(applications);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> CreateApplication([FromBody] AddApplicationDTO applicationDTO)
        {
            await _applicationService.CreateApplicationAsync(applicationDTO);
            var response = new SuccessResponse<string>("Application submitted successfully", 201);
            return Created("", response);
        }

        [HttpPatch("{applicationId}/status")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> UpdateApplicationStatus(Guid applicationId, [FromBody] UpdateApplicationStatusDTO statusDTO)
        {
            await _applicationService.UpdateApplicationStatusAsync(applicationId, statusDTO);
            var response = new SuccessResponse<string>("Application status updated successfully");
            return Ok(response);
        }
    }
}