using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Auth;

namespace Application.Services_Interfaces
{
    public interface IUserService
    {
        public  Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
        public Task<AuthResponseDTO> SignupAsync(SignupDTO signupDto);


    }
}
