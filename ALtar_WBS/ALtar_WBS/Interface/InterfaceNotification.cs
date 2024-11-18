using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceNotification
    {
        public Task<Notifications> CreateNotification(string message, string type);

        public Task<bool> SendNotificationToUser(int userId, int notificationId);

        public Task<IEnumerable<Notifications>> GetUserNotifications(int userId);

        public Task<Notifications> GetNotificationById(int notificationId);

        public Task<IEnumerable<Notifications>> GetAllNotifications();

        public Task<bool> DeleteNotification(int notificationId);

        public Task<bool> SendEmailNotification(int userId, string message);
    }
}
