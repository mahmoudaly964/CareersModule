using Application.DTOs.Auth;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.UserUseCase;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly ISignUpUseCase _signUpUseCase;
        private readonly ILogInUseCase _logInUseCase;
        private readonly ISignUpAdminUseCase _signUpAdminUseCase;
        public UserService(
            ISignUpUseCase signUpUseCase,
            ILogInUseCase logInUseCase,
            ISignUpAdminUseCase signUpAdminUseCase)
        {
            _signUpUseCase = signUpUseCase;
            _logInUseCase = logInUseCase;
            _signUpAdminUseCase = signUpAdminUseCase;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            return await _logInUseCase.ExecuteAsync(loginDto);
        }

        public async Task<AuthResponseDTO> SignupAsync(SignupDTO signupDto)
        {
            return await _signUpUseCase.ExecuteAsync(signupDto);
        }
        public async Task<AuthResponseDTO> SignupAdminAsync(SignupDTO signupAdminDto)
        {
            return await _signUpAdminUseCase.ExecuteAsync(signupAdminDto);
        }
    }
}