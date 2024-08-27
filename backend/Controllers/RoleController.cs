using Microsoft.AspNetCore.Mvc;
using Scribble.Services;

[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var roles = await _roleService.GetRolesAsync();
        if (roles == null) return NotFound("No roles were found");

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null) return NotFound("No role was found");

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        var result = await _roleService.CreateRoleAsync(roleName);
        if (result.Succeeded) return Ok("Role created successfully");

        return BadRequest("Role creation failed");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string roleName)
    {
        var result = await _roleService.DeleteRoleAsync(roleName);
        if (result.Succeeded) return Ok("Role deleted successfully");

        return BadRequest("Role deletion failed");
    }
}