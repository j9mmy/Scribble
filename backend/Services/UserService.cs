using Microsoft.AspNetCore.Identity;
using Scribble.Models;
using Scribble.Repositories;

namespace Scribble.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var result = await _userRepository.DeleteUserAsync(user);
            return result;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return users;
        }

        public async Task<IdentityResult> UpdateUserAsync(string id) // TODO: Add user object to update
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var result = await _userRepository.UpdateUserAsync(user);
            return result;
        }
    }
}