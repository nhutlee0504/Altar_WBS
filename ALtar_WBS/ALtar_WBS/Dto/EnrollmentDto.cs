using ALtar_WBS.Model;

namespace ALtar_WBS.Dto
{
	public class EnrollmentDto
	{
		public string status { get; set; }
		public DateTime ErollmentDate { get; set; }
		public string PaymentStatus { get; set; }
		public int StudentID { get; set; }
		public int ClassID { get; set; }
	}
}
