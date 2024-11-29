using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class Teacher
    {
		[Key]
		[ForeignKey("User")]
		public int UserID { get; set; } // UserID là khóa chính
		public User User { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string IdCard { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Subject { get; set; }
		public string ProfileImage { get; set; }
		public DateTime StartDate { get; set; }
		public ICollection<TeacherSalary> TeacherSalaries { get; set; }
		public ICollection<ClassTeachers> ClassTeachers { get; set; }
	}
}
