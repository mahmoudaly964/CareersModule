using Application.DTOs.Auth;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.UserUseCase;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly ISignUpUseCase _signUpUseCase;
        private readonly ILogInUseCase _logInUseCase;
        public UserService(
            ISignUpUseCase signUpUseCase,
            ILogInUseCase logInUseCase)
        {
            _signUpUseCase = signUpUseCase;
            _logInUseCase = logInUseCase;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            return await _logInUseCase.ExecuteAsync(loginDto);
        }

        public async Task<AuthResponseDTO> SignupAsync(SignupDTO signupDto)
        {
            return await _signUpUseCase.ExecuteAsync(signupDto);
        }
    }
}