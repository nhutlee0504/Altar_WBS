using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class Notifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime Senđate { get; set; }
        public ICollection<UserNotifications> UserNotifications { get; set; }
    }
}
