using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly InterfaceUser _userService;

        public UserController(InterfaceUser userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromForm] UserDto userDto)
        {
            try
            {
                var user = await _userService.AddUser(userDto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("lock/{userId}")]
        public async Task<IActionResult> LockUserAccount(int userId)
        {
            try
            {
                var success = await _userService.LockUserAccount(userId);
                return Ok(success);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("unlock/{userId}")]
        public async Task<IActionResult> UnlockUserAccount(int userId)
        {
            try
            {
                var success = await _userService.UnlockUserAccount(userId);
                return Ok(success);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("reset-password/{userId}")]
        public async Task<IActionResult> ResetPassword(int userId, string newPassword)
        {
            try
            {
                var success = await _userService.ResetPassword(userId, newPassword);
                return Ok(success);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers(string keyword)
        {
            try
            {
                var users = await _userService.SearchUsers(keyword);
                return Ok(users);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromForm] UserDto userDto)
        {
            try
            {
                var user = await _userService.UpdateUser(userId, userDto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
