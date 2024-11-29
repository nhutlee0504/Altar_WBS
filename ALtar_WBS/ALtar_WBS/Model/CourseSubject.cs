using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ALtar_WBS.Model
{
    public class CourseSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int csID { get; set; }
        public int CourseID { get; set; }
        [JsonIgnore]
        public Course Course { get; set; }
        public int SubjectID { get; set; }
        public Subjects Subjects { get; set; }
    }
}
