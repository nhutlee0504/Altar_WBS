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
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SmtpUsername = "leminhnhut718@gmail.com";
        private const string SmtpPassword = "yvhblqdkefcjmyvl";
        private const string SmtpFromAddress = "leminhnhut718@gmail.com";

        public ServiceNotification(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tạo thông báo mới
        public async Task<Notifications> CreateNotification(string message, string type)
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

        // Xóa thông báo
        public async Task<bool> DeleteNotification(int notificationId)
        {
            var notification = await _context.notifications.FindAsync(notificationId);
            if (notification == null) return false;

            _context.notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        // Lấy tất cả thông báo
        public async Task<IEnumerable<Notifications>> GetAllNotifications()
        {
            return await _context.notifications.ToListAsync();
        }

        // Lấy thông báo theo ID
        public async Task<Notifications> GetNotificationById(int notificationId)
        {
            return await _context.notifications.FindAsync(notificationId);
        }

        // Lấy tất cả thông báo của một người dùng
        public async Task<IEnumerable<Notifications>> GetUserNotifications(int userId)
        {
            return await _context.userNotifications
                .Where(un => un.UserID == userId)
                .Include(un => un.Notifications)
                .Select(un => un.Notifications)
                .ToListAsync();
        }

        // Gửi thông báo qua email
        public async Task<bool> SendEmailNotification(int userId, string message)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null || string.IsNullOrEmpty(user.Email))
                return false;

            // Tạo nội dung email
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Hệ Thống", SmtpFromAddress));
            emailMessage.To.Add(new MailboxAddress(user.UserName, user.Email));
            emailMessage.Subject = "Thông báo từ Hệ thống";
            emailMessage.Body = new TextPart("plain") { Text = message };

            // Sử dụng MailKit để gửi email
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(SmtpUsername, SmtpPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gửi email thất bại: {ex.Message}");
                    return false;
                }
            }
        }

        // Gửi thông báo tới một người dùng
        public async Task<bool> SendNotificationToUser(int userId, int notificationId)
        {
            var user = await _context.users.FindAsync(userId);
            var notification = await _context.notifications.FindAsync(notificationId);
            if (user == null || notification == null) return false;

            var userNotification = new UserNotifications
            {
                UserID = userId,
                NotificationID = notificationId
            };

            _context.userNotifications.Add(userNotification);
            await _context.SaveChangesAsync();

            // Gửi thông báo qua email cho người dùng
            await SendEmailNotification(userId, notification.Message);
            return true;
        }
    }
}
