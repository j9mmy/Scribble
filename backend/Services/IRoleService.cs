using Microsoft.AspNetCore.Identity;
using Scribble.Models;

namespace Scribble.Services
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetRolesAsync();
        Task<IdentityRole?> GetRoleByIdAsync(string id);
        Task<IdentityRole?> GetRoleByNameAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleName);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
    }
}