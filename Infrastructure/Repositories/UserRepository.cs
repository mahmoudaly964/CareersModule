using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>>_roleManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
            : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> VerifyPasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password,string? role=null)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create user: {errors}");
            }
            if(role!=null)
            {
                if (!await _roleManager.RoleExistsAsync("ADMIN"))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>("ADMIN"));
                }
                await _userManager.AddToRoleAsync(user, "ADMIN");
            }
            return user;
        }
        public async Task<string?> GetRoleAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
    }

}