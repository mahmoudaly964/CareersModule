using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacancyController : ControllerBase
    {
        [HttpGet("{vacancyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetVacancyById(Guid vacancyId)
        {            
            return Ok();
        }
    }
}
