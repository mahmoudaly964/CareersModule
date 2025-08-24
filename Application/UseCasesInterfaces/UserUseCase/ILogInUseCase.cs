using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.UserUseCase
{
    public interface ILogInUseCase
    {
        public Task<AuthResponseDTO> ExecuteAsync(LoginDTO loginDto);
    }
}
