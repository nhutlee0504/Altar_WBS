using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ALtar_WBS.Model
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Fee { get; set; }
        public int Duration { get; set; }
        public ICollection<CourseSubject> CourseSubjects { get; set; }
        [JsonIgnore]
        public ICollection<Classes> Classes { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
