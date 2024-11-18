using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly InterfaceNotification _notificationService;

        public NotificationsController(InterfaceNotification notificationService)
        {
            _notificationService = notificationService;
        }

        // Tạo thông báo mới
        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromBody] string message, [FromQuery] string type)
        {
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(type))
            {
                return BadRequest("Thông báo và loại không được để trống.");
            }

            var notification = await _notificationService.CreateNotification(message, type);
            if (notification == null)
            {
                return StatusCode(500, "Không thể tạo thông báo.");
            }

            return Ok(notification);
        }

        // Xóa thông báo
        [HttpDelete("delete/{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            var isDeleted = await _notificationService.DeleteNotification(notificationId);
            if (!isDeleted)
            {
                return NotFound($"Thông báo với ID {notificationId} không tồn tại.");
            }

            return NoContent();
        }

        // Lấy tất cả thông báo
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationService.GetAllNotifications();
            return Ok(notifications);
        }

        // Lấy thông báo theo ID
        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationById(int notificationId)
        {
            var notification = await _notificationService.GetNotificationById(notificationId);
            if (notification == null)
            {
                return NotFound($"Thông báo với ID {notificationId} không tồn tại.");
            }

            return Ok(notification);
        }

        // Lấy tất cả thông báo của người dùng
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotifications(userId);
            if (notifications == null)
            {
                return NotFound($"Không có thông báo nào cho người dùng với ID {userId}.");
            }

            return Ok(notifications);
        }

        // Gửi thông báo qua email
        [HttpPost("sendEmail/{userId}")]
        public async Task<IActionResult> SendEmailNotification(int userId, [FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Nội dung thông báo không được để trống.");
            }

            var result = await _notificationService.SendEmailNotification(userId, message);
            if (!result)
            {
                return StatusCode(500, "Gửi email thất bại.");
            }

            return Ok("Email đã được gửi thành công.");
        }

        // Gửi thông báo tới người dùng và gửi email
        [HttpPost("sendToUser/{userId}/{notificationId}")]
        public async Task<IActionResult> SendNotificationToUser(int userId, int notificationId)
        {
            var result = await _notificationService.SendNotificationToUser(userId, notificationId);
            if (!result)
            {
                return StatusCode(500, "Gửi thông báo đến người dùng thất bại.");
            }

            return Ok("Thông báo đã được gửi thành công và email đã được gửi.");
        }
    }
}
