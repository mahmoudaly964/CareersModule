using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services_Interfaces
{
    public interface IJwtService
    {
        public string GenerateAccessToken(ApplicationUser user,string? role=null);
        public string GenerateRefreshToken(ApplicationUser user);
        public bool ValidateToken(string token);
    }
}
