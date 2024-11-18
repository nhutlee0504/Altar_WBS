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

        // Thêm một người dùng mới
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var user = await _userService.AddUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserID }, user);
        }

        // Thêm nhiều người dùng cùng lúc
        [HttpPost("add-multiple")]
        public async Task<IActionResult> AddUsers([FromBody] List<UserDto> userDtos)
        {
            var users = await _userService.AddUsers(userDtos);
            return Ok(users);
        }

        // Lấy danh sách tất cả người dùng
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // Lấy thông tin người dùng theo ID
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Khóa tài khoản người dùng
        [HttpPatch("lock/{userId}")]
        public async Task<IActionResult> LockUserAccount(int userId)
        {
            var success = await _userService.LockUserAccount(userId);
            if (!success) return NotFound("User not found.");
            return Ok("User account locked.");
        }

        // Mở khóa tài khoản người dùng
        [HttpPatch("unlock/{userId}")]
        public async Task<IActionResult> UnlockUserAccount(int userId)
        {
            var success = await _userService.UnlockUserAccount(userId);
            if (!success) return NotFound("User not found.");
            return Ok("User account unlocked.");
        }

        // Cài đặt lại mật khẩu người dùng
        [HttpPatch("reset-password/{userId}")]
        public async Task<IActionResult> ResetPassword(int userId, [FromBody] string newPassword)
        {
            var success = await _userService.ResetPassword(userId, newPassword);
            if (!success) return NotFound("User not found.");
            return Ok("Password reset successful.");
        }

        // Tìm kiếm người dùng theo từ khóa
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers([FromQuery] string keyword)
        {
            var users = await _userService.SearchUsers(keyword);
            return Ok(users);
        }

        // Cập nhật thông tin người dùng
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            var user = await _userService.UpdateUser(userId, userDto);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        // Kiểm tra xem người dùng có tồn tại không
        [HttpGet("exists/{userId}")]
        public async Task<IActionResult> UserExists(int userId)
        {
            var exists = await _userService.UserExists(userId);
            return Ok(exists);
        }
    }
}
