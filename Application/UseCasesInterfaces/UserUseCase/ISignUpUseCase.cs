using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.UserUseCase
{
    public interface ISignUpUseCase
    {
        public Task<AuthResponseDTO> ExecuteAsync(SignupDTO signupDto);

    }
}
