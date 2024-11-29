namespace ALtar_WBS.Dto
{
    public class ScheduleDto
    {
        public string DayOfWeek { get; set; } // "Thứ 2", "Thứ 3", ...
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Room { get; set; }
        public int ClassID { get; set; }
    }
}
