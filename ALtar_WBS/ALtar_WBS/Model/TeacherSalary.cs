using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
	public class TeacherSalary
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SalaryID { get; set; }
		public decimal amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public string PaymentType { get; set; }
		public string Status { get; set; }
		public int TeacherID { get; set; }
		public Teacher Teacher { get; set; }
	}
}
