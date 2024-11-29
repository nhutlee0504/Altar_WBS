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
            try
            {
                var roles = await _roleService.GetAllRoles();
                return Ok(roles);
            }
            catch (NotImplementedException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            try
            {
                var role = await _roleService.GetRoleById(roleId);
                return Ok(role);
            }
            catch (NotImplementedException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            try
            {
                var createdRole = await _roleService.CreateRole(roleName);
                return Ok(createdRole);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(int roleId, string roleName)
        {
            try
            {
                var updatedRole = await _roleService.UpdateRole(roleId, roleName);
                return Ok(updatedRole);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            try
            {
                var result = await _roleService.DeleteRole(roleId);
                return Ok(result);
            }
            catch (NotImplementedException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("copy/{roleId}")]
        public async Task<IActionResult> CopyRole(int roleId)
        {
            try
            {
                var newRole = await _roleService.CopyRole(roleId);
                return Ok(newRole);
            }
            catch (NotImplementedException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser( int userId, int roleId)
        {
            try
            {
                var result = await _roleService.AssignRoleToUser(userId, roleId);
                return Ok(result);
            }
            catch (NotImplementedException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
