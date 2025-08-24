using Application.DTOs.Auth;
using Application.Exceptions;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.UserUseCase;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public class LogInUseCase : ILogInUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public LogInUseCase(
            IUserRepository userRepository,
            IJwtService jwtService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthResponseDTO> ExecuteAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new NotFoundException("Invalid email or password");
            }

            var isPasswordValid = await _userRepository.VerifyPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                throw new NotFoundException("Invalid email or password");
            }

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken(user);

            return new AuthResponseDTO
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
