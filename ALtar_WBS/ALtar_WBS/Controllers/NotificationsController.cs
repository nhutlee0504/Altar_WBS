using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification(string message, string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(type))
                {
                    return BadRequest("Message and type cannot be empty");
                }

                var notification = await _notificationService.CreateNotification(message, type);
                return Ok(notification);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            try
            {
                var isDeleted = await _notificationService.DeleteNotification(notificationId);
                if (!isDeleted)
                {
                    return NotFound($"Notification with ID {notificationId} not found");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                var notifications = await _notificationService.GetAllNotifications();
                return Ok(notifications);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationById(int notificationId)
        {
            try
            {
                var notification = await _notificationService.GetNotificationById(notificationId);
                if (notification == null)
                {
                    return NotFound($"Notification with ID {notificationId} not found");
                }

                return Ok(notification);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            try
            {
                var notifications = await _notificationService.GetUserNotifications(userId);
                if (notifications == null)
                {
                    return NotFound($"No notifications found for user with ID {userId}");
                }

                return Ok(notifications); 
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }


        [HttpPost("sendEmail/{userId}")]
        public async Task<IActionResult> SendEmailNotification(int userId, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                {
                    return BadRequest("Message content cannot be empty");
                }

                var result = await _notificationService.SendEmailNotification(userId, message);
                if (!result)
                {
                    return StatusCode(500, "Failed to send email");
                }

                return Ok("Email sent successfully"); 
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost("sendToUser/{userId}/{notificationId}")]
        public async Task<IActionResult> SendNotificationToUser(int userId, int notificationId)
        {
            try
            {
                var result = await _notificationService.SendNotificationToUser(userId, notificationId);
                if (!result)
                {
                    return StatusCode(500, "Failed to send notification to user");
                }

                return Ok("Notification sent successfully and email was sent"); 
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }
    }
}
