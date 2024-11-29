using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ALtar_WBS.Model
{
    public class Classes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassID { get; set; }
        public int CourseID { get; set; }
        [JsonIgnore]
        public Course Course { get; set; }
        public ICollection<ClassTeachers> ClassTeachers { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Schedule> schedules { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
