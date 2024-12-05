using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceNotification : InterfaceNotification
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private const string SmtpHost = "smtp.gmail.com";
		private const int SmtpPort = 587;
		private const string SmtpUsername = "leminhnhut718@gmail.com";
		private const string SmtpPassword = "yvhblqdkefcjmyvl";
		private const string SmtpFromAddress = "leminhnhut718@gmail.com";

		public ServiceNotification(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Notifications> CreateNotification(string message, string type)
		{
			try
			{
				var notification = new Notifications
				{
					Message = message,
					Type = type,
					Senđate = DateTime.UtcNow
				};

				_context.notifications.Add(notification);
				await _context.SaveChangesAsync();
				return notification;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error creating notification: {ex.Message}");
			}
		}

		public async Task<bool> DeleteNotification(int notificationId)
		{
			try
			{
				var notification = await _context.notifications.FindAsync(notificationId);
				if (notification == null)
					throw new InvalidOperationException("Notification not found");

				_context.notifications.Remove(notification);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error deleting notification: {ex.Message}");
			}
		}

		public async Task<IEnumerable<Notifications>> GetAllNotifications()
		{
			try
			{
				return await _context.notifications.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error fetching notifications: {ex.Message}");
			}
		}

		public async Task<Notifications> GetNotificationById(int notificationId)
		{
			try
			{
				var notification = await _context.notifications.FindAsync(notificationId);
				if (notification == null)
					throw new InvalidOperationException("Notification not found");

				return notification;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error fetching notification by ID: {ex.Message}");
			}
		}

		public async Task<IEnumerable<Notifications>> GetUserNotifications(int userId)
		{
			try
			{
				return await _context.userNotifications
					.Where(un => un.UserID == userId)
					.Include(un => un.Notifications)
					.Select(un => un.Notifications)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error fetching user notifications: {ex.Message}");
			}
		}

		public async Task<bool> SendEmailNotification(int userId, string message)
		{
			try
			{
				var user = await _context.users.FindAsync(userId);
				if (user == null || string.IsNullOrEmpty(user.Email))
					throw new InvalidOperationException("User not found or email is missing");

				var emailMessage = new MimeMessage();
				emailMessage.From.Add(new MailboxAddress("Hệ Thống", SmtpFromAddress));
				emailMessage.To.Add(new MailboxAddress(user.UserName, user.Email));
				emailMessage.Subject = "Thông báo từ Hệ thống";
				emailMessage.Body = new TextPart("plain") { Text = message };

				using (var client = new MailKit.Net.Smtp.SmtpClient())
				{
					await client.ConnectAsync(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
					await client.AuthenticateAsync(SmtpUsername, SmtpPassword);
					await client.SendAsync(emailMessage);
					await client.DisconnectAsync(true);
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error sending email notification: {ex.Message}");
			}
		}

		public async Task<bool> SendNotificationToUser(int userId, int notificationId)
		{
			try
			{
				var user = await _context.users.FindAsync(userId);
				var notification = await _context.notifications.FindAsync(notificationId);
				if (user == null || notification == null)
					throw new InvalidOperationException("User or Notification not found");

				var userNotification = new UserNotifications
				{
					UserID = userId,
					NotificationID = notificationId
				};

				_context.userNotifications.Add(userNotification);
				await _context.SaveChangesAsync();

				await SendEmailNotification(userId, notification.Message);
				return true;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error sending notification to user: {ex.Message}");
			}
		}
	}
}
