using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class UserNotifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int upID { get; set; }
        public int NotificationID { get; set; }
        public Notifications Notifications { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
