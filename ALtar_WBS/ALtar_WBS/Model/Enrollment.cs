using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }
        public string status { get; set; }
        public DateTime ErollmentDate { get; set; }
        public string PaymentStatus { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int ClassID { get; set; }
        public Classes Classes { get; set; }
    }
}
