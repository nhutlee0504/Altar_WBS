using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly InterfaceRole _roleService;

        public RoleController(InterfaceRole roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var role = await _roleService.GetRoleById(roleId);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var createdRole = await _roleService.CreateRole(roleName);
            return CreatedAtAction(nameof(GetRoleById), new { roleId = createdRole.RoleID }, createdRole);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole([FromBody]int roleId, string roleName)
        {
            var updatedRole = await _roleService.UpdateRole(roleId, roleName);
            if (updatedRole == null)
                return NotFound();

            return Ok(updatedRole);
        }

        [HttpDelete("delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            var result = await _roleService.DeleteRole(roleId);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("copy/{roleId}")]
        public async Task<IActionResult> CopyRole(int roleId)
        {
            var newRole = await _roleService.CopyRole(roleId);
            return CreatedAtAction(nameof(GetRoleById), new { roleId = newRole.RoleID }, newRole);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromQuery] int userId, [FromQuery] int roleId)
        {
            var result = await _roleService.AssignRoleToUser(userId, roleId);
            if (!result)
                return BadRequest("Failed to assign role to user");

            return Ok(new { message = "Role assigned successfully" });
        }
    }
}
