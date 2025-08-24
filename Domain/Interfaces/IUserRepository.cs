using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> VerifyPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password);
    }
}
