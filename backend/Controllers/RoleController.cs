using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        if (roles == null) return NotFound();

        return Ok(roles);
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(string role)
    {
        if (role == null) return BadRequest("Invalid role received");

        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (roleExists) return BadRequest("Role already exists");

        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        if (result.Succeeded) return Ok("Role successfully created");

        return BadRequest("Failed to create new role");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(string role)
    {
        if (role == null) return BadRequest("Invalid role received");

        var existingRole = await _roleManager.FindByNameAsync(role);
        if (existingRole == null) return BadRequest("Entered role does not exist");

        var result = await _roleManager.DeleteAsync(existingRole);
        if (result.Succeeded) return Ok("Role successfully deleted");

        return BadRequest("Failed to delete role");
    }
}