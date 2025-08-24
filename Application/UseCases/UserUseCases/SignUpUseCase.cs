using Application.DTOs.Auth;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.UserUseCase;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public class SignUpUseCase : ISignUpUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public SignUpUseCase(
            IUserRepository userRepository,
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthResponseDTO> ExecuteAsync(SignupDTO signupDto)
        {
            if (await _userRepository.EmailExistsAsync(signupDto.Email))
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var user = new ApplicationUser
            {
                UserName = signupDto.Email,
                Email = signupDto.Email,
                FullName = signupDto.FullName,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateUserAsync(user, signupDto.Password);
            await _unitOfWork.SaveChangesAsync();

            var accessToken = _jwtService.GenerateAccessToken(createdUser);
            var refreshToken = _jwtService.GenerateRefreshToken(createdUser);

            return new AuthResponseDTO
            {
                UserId = createdUser.Id,
                FullName = createdUser.FullName,
                Email = createdUser.Email!,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
