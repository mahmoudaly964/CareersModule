using Application.DTOs.Auth;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.UserUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public class SignUpAdminUseCase : ISignUpAdminUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SignUpAdminUseCase(
            IUserRepository userRepository,
            IJwtService jwtService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AuthResponseDTO> ExecuteAsync(SignupDTO signupDto)
        {
            if (await _userRepository.EmailExistsAsync(signupDto.Email))
            {
                throw new InvalidOperationException("User with this email already exists");
            }
            var user = _mapper.Map<ApplicationUser>(signupDto);
            user.CreatedAt = DateTime.UtcNow;

            var createdUser = await _userRepository.CreateUserAsync(user, signupDto.Password,"admin");
            await _unitOfWork.SaveChangesAsync();

            var accessToken = _jwtService.GenerateAccessToken(createdUser,"ADMIN");
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
