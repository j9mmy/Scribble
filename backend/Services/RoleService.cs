using Microsoft.AspNetCore.Identity;
using Scribble.Models;
using Scribble.Repositories;

namespace Scribble.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public RoleService(IRoleRepository roleRepository, UserManager<ApplicationUser> userManager)
        {
            _roleRepository = roleRepository;
            _userManager = userManager;
        }

        public Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            var result = _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var result = await _roleRepository.CreateRoleAsync(roleName);
            return result;
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);
            if (role == null) return IdentityResult.Failed(new IdentityError { Description = "Role not found." });

            var result = await _roleRepository.DeleteRoleAsync(role);
            return result;
        }

        public async Task<IdentityRole?> GetRoleByIdAsync(string id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            return role;
        }

        public async Task<IdentityRole?> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);
            return role;
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetRolesAsync();
            return roles;
        }

        public Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
            var result = _roleRepository.UpdateRoleAsync(role);
            return result;
        }
    }
}