using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ALtar_WBS.Model
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public string? Status { get; set; } // "Có mặt", "Vắng mặt", "Trễ"
        public string? Remarks { get; set; }
        public Student Student { get; set; }
        public Classes Classes { get; set; }
    }
}
