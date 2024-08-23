using Microsoft.AspNetCore.Mvc;
using Scribble.Models;
using Scribble.Services;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;
    
    public UserController(IUserService userService, IAuthenticationService authenticationService)
    {
        _userService = userService;
        _authenticationService = authenticationService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound("No user was found");

        return Ok(user);
    }

    [HttpGet]
    public async Task <IActionResult> Get()
    {
        var users = await _userService.GetUsersAsync();
        if (users == null) return NotFound("No users were found");

        return Ok(users);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authenticationService.LoginUserAsync(request);
        if (!result.Succeeded) return BadRequest("Invalid login attempt");

        return Ok("User logged in succesfully");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _authenticationService.SignOutUserAsync();
        return Ok("User logged out succesfully");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authenticationService.RegisterUserAsync(request);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok("User created succesfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok("User deleted succesfully");
    }
}