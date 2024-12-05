namespace ALtar_WBS.Dto
{
    public class AttendaceDto
    {
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
    }
}
