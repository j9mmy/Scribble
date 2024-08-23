using Microsoft.AspNetCore.Identity;
using Scribble.Models;
using Scribble.Repositories;

namespace Scribble.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoleService _roleService;
        
        public AuthenticationService(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, IRoleService roleService)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _roleService = roleService;
        }

        public async Task<SignInResult> LoginUserAsync(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            return result;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest request)
        {
            var role = await _roleService.GetRoleByNameAsync(request.Role);
            if (role == null) return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });

            var newUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                Email = request.Email,
            };

            var result = await _userRepository.CreateUserAsync(newUser, request.Password);
            if (!result.Succeeded) return result;

            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            var roleResult = await _roleService.AddUserToRoleAsync(user, request.Role);
            if (!roleResult.Succeeded) return roleResult;

            return IdentityResult.Success;
        }

        public Task SignOutUserAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}