using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public float GradeValue { get; set; }
        public string? Remarks { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
