using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using ALtar_WBS.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly InterfaceAuth _authService;

        public AuthController(InterfaceAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin([FromForm] LoginDto loginDto)
        {
            var token = await _authService.LoginAdmin(loginDto);
            if (token != null)
            {
                return Ok(new { token });
            }
            return Unauthorized(new { message = "Invalid admin credentials or insufficient permissions." });
        }


        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromForm] LoginDto loginDto)
        {
            var token = await _authService.LoginUser(loginDto);
            if (token != null)
            {
                return Ok(new { token });
            }
            return Unauthorized(new { message = "Invalid user credentials." });
        }

        [HttpGet("getUserInfo")]
        public ActionResult<User> GetUserInfo()
        {
            try
            {
                var userInfo = _authService.GetUserInfoFromToken();
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
