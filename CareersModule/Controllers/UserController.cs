using Application.DTOs.Auth;
using Application.Responses;
using Application.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareersModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        [ProducesResponseType(typeof(SuccessResponse<AuthResponseDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<AuthResponseDTO>>> Signup([FromBody] SignupDTO signupDto)
        {
            var authResponse = await _userService.SignupAsync(signupDto);
            var response = new SuccessResponse<AuthResponseDTO>(authResponse, 201);
            return Created("", response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(SuccessResponse<AuthResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SuccessResponse<AuthResponseDTO>>> Login([FromBody] LoginDTO loginDto)
        {
            var authResponse = await _userService.LoginAsync(loginDto);
            var response = new SuccessResponse<AuthResponseDTO>(authResponse);
            return Ok(response);
        }
    }
}