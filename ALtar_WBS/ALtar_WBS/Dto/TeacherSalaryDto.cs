namespace ALtar_WBS.Dto
{
	public class TeacherSalaryDto
	{
		public decimal amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public string PaymentType { get; set; }
		public string Status { get; set; }
		public int TeacherID { get; set; }
	}
}
