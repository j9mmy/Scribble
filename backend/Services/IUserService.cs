using Microsoft.AspNetCore.Identity;
using Scribble.Models;

namespace Scribble.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<IdentityResult> UpdateUserAsync(string id);
        Task<IdentityResult> DeleteUserAsync(string id);
    }
}