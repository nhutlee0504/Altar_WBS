namespace ALtar_WBS.Dto
{
    public class PaymentDto
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; }
        public int StudentID { get; set; }
    }
}
