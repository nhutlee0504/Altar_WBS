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
        public async Task<IActionResult> AddUser([FromForm] UserDto userDto)
        {
            try
            {
                var user = await _userService.AddUser(userDto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Lấy danh sách tất cả người dùng
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Lấy thông tin người dùng theo ID
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Khóa tài khoản người dùng
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Mở khóa tài khoản người dùng
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Cài đặt lại mật khẩu người dùng
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Tìm kiếm người dùng theo từ khóa
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }

        // Cập nhật thông tin người dùng
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
                return BadRequest(ex.Message); // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi khác nếu có
            }
        }
    }
}
