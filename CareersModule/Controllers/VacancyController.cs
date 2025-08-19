using Application.DTOs.Vacancy;
using Application.Responses;
using Application.Services_Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet("{vacancyId}")]
        [ProducesResponseType(typeof(SuccessResponse<VacancyResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<VacancyResponseDTO>>> GetVacancyById(Guid vacancyId)
        {
            var vacancy = await _vacancyService.GetVacancyByIdAsync(vacancyId);
            var response =new SuccessResponse<VacancyResponseDTO>(vacancy); 
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<VacancyResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<IEnumerable<VacancyResponseDTO>>>> GetVacancies(
            [FromQuery] string? role,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var vacancies = await _vacancyService.GetAllVacanciesAsync(role, pageNumber, pageSize);
            var response = new SuccessResponse<IEnumerable<VacancyResponseDTO>>(vacancies);
            return Ok(response);
        }
        [HttpGet("published")]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<VacancyResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<IEnumerable<VacancyResponseDTO>>>> GetPublishedVacancies(
            [FromQuery] string? role,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var vacancies = await _vacancyService.GetAllPublishedVacanciesAsync(role, pageNumber, pageSize);
            var response = new SuccessResponse<IEnumerable<VacancyResponseDTO>>(vacancies);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> CreateVacancy([FromBody] AddVacancyDTO vacancy)
        {
            await _vacancyService.CreateVacancyAsync(vacancy);
            var response = new SuccessResponse<string>("Vacancy created successfully",201);
            return Created("",response);
        }

        [HttpPut("{vacancyId}")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> UpdateVacancy(Guid vacancyId, [FromBody] UpdateVacancyDTO updateVacancyDTO)
        {
            await _vacancyService.UpdateVacancyAsync(updateVacancyDTO, vacancyId);
            var response = new SuccessResponse<string>("Vacancy updated successfully");
            return Ok(response);
        }

        [HttpDelete("{vacancyId}")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> DeleteVacancy(Guid vacancyId)
        {
            await _vacancyService.DeleteVacancyAsync(vacancyId);
            var response = new SuccessResponse<string>("Vacancy deleted successfully",204);
            return StatusCode(204, response);
        }

        [HttpPatch("{vacancyId}/publish")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> PublishVacancy(Guid vacancyId)
        {
            await _vacancyService.PublishVacancyAsync(vacancyId);
            var response = new SuccessResponse<string>("Vacancy published successfully");
            return Ok(response);
        }

        [HttpPatch("{vacancyId}/unpublish")]
        [ProducesResponseType(typeof(SuccessResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<string>>> UnpublishVacancy(Guid vacancyId)
        {
            await _vacancyService.UnPublishVacancyAsync(vacancyId);
            var response = new SuccessResponse<string>("Vacancy unpublished successfully");
            return Ok(response);
        }
    }
}