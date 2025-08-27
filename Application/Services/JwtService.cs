using Application.Services_Interfaces;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _jwtSecret;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public JwtService()
        {
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") 
                ?? throw new InvalidOperationException("JWT_SECRET_KEY environment variable is not configured");
            _jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") 
                ?? throw new InvalidOperationException("JWT_ISSUER environment variable is not configured");
            _jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") 
                ?? throw new InvalidOperationException("JWT_AUDIENCE environment variable is not configured");
        }

        public string GenerateAccessToken(ApplicationUser user,string?role=null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),        
                new(ClaimTypes.Name, user.UserName ?? string.Empty),       
                new(ClaimTypes.Email, user.Email ?? string.Empty),                   
            };
            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),                      
                Expires = DateTime.UtcNow.AddMinutes(60),  
                Issuer = _jwtIssuer,                                       
                Audience = _jwtAudience,                                   
                SigningCredentials = new SigningCredentials(          
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(ApplicationUser user)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtIssuer,
                    ValidAudience = _jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret))
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
