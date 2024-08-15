using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly AppDbContext DbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(AppDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        DbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await DbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return NotFound();

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (!result.Succeeded) return Unauthorized();

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await DbContext.Users.FindAsync(id);
        if (user == null) return NotFound();
        
        DbContext.Users.Remove(user);
        await DbContext.SaveChangesAsync();

        return Ok();
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    // TODO: Add custom user properties
}