namespace ALtar_WBS.Interface
{
    public interface IStatisticsService
    {
        Task<int> GetTotalStudentsAsync();
        Task<int> GetActiveStudentsAsync();
        Task<int> GetGraduatedStudentsAsync();
        Task<decimal> GetTotalStudentPaymentsAsync();

        Task<int> GetTotalTeachersAsync();
        Task<int> GetActiveTeachersAsync();
        Task<decimal> GetTotalTeacherSalariesAsync();

        Task<int> GetTotalCoursesAsync();
        Task<int> GetOngoingCoursesAsync();
        Task<int> GetCompletedCoursesAsync();
        Task<decimal> GetTotalCourseRevenueAsync();

        Task<int> GetTotalClassesAsync();
        Task<int> GetActiveClassesAsync();
        Task<int> GetTotalEnrollmentsAsync();

        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> GetTotalExpensesAsync();
        Task<decimal> GetNetProfitAsync();

        Task<int> GetTotalAttendanceRecordsAsync();
        Task<int> GetAbsentRecordsAsync();
        Task<int> GetLateRecordsAsync();

        Task<int> GetTotalNotificationsAsync();
        Task<int> GetUnreadNotificationsAsync(int userId);

        Task<byte[]> ExportDataToExcelAsync<T>(string sheetName, IEnumerable<T> data, Dictionary<string, string> columnMappings = null);
    }
}
