using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class ClassTeachers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ctID { get; set; }
        public int ClassID { get; set; }
        public Classes Classes { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}
