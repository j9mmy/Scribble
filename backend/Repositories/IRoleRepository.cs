using Microsoft.AspNetCore.Identity;

namespace Scribble.Repositories
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetRolesAsync();
        Task<IdentityRole?> GetRoleByIdAsync(string id);
        Task<IdentityRole?> GetRoleByNameAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
    }
}