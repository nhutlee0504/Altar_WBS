using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ALtar_WBS.Model
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleID { get; set; }
        public string DayOfWeek { get; set; } // "Thứ 2", "Thứ 3", ...
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Room { get; set; }
        public int ClassID { get; set; }
        public Classes Classes { get; set; }
    }
}
