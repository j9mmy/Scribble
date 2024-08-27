using Microsoft.AspNetCore.Identity;
using Scribble.Models;

namespace Scribble.Services
{
    public interface IAuthenticationService
    {
        Task<SignInResult> LoginUserAsync(LoginRequest request);
        Task<IdentityResult> RegisterUserAsync(RegisterRequest request);
        Task SignOutUserAsync();
    }
}