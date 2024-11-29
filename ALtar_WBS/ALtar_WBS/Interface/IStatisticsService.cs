namespace ALtar_WBS.Interface
{
	public interface IStatisticsService
	{
		// Thống kê học viên
		Task<int> GetTotalStudentsAsync(); // Tổng số học viên
		Task<int> GetActiveStudentsAsync(); // Số học viên đang tham gia học
		Task<int> GetGraduatedStudentsAsync(); // Số học viên đã tốt nghiệp
		Task<decimal> GetTotalStudentPaymentsAsync(); // Tổng số tiền học viên đã đóng

		// Thống kê giáo viên
		Task<int> GetTotalTeachersAsync(); // Tổng số giáo viên
		Task<int> GetActiveTeachersAsync(); // Số giáo viên đang giảng dạy
		Task<decimal> GetTotalTeacherSalariesAsync(); // Tổng số tiền lương giáo viên đã trả

		// Thống kê khóa học
		Task<int> GetTotalCoursesAsync(); // Tổng số khóa học
		Task<int> GetOngoingCoursesAsync(); // Số khóa học đang diễn ra
		Task<int> GetCompletedCoursesAsync(); // Số khóa học đã hoàn thành
		Task<decimal> GetTotalCourseRevenueAsync(); // Tổng doanh thu từ khóa học

		// Thống kê lớp học
		Task<int> GetTotalClassesAsync(); // Tổng số lớp học
		Task<int> GetActiveClassesAsync(); // Số lớp học đang hoạt động
		Task<int> GetTotalEnrollmentsAsync(); // Tổng số học viên đăng ký lớp

		// Thống kê doanh thu
		Task<decimal> GetTotalRevenueAsync(); // Tổng doanh thu
		Task<decimal> GetTotalExpensesAsync(); // Tổng chi phí (bao gồm lương giáo viên)
		Task<decimal> GetNetProfitAsync(); // Lợi nhuận ròng (Doanh thu - Chi phí)

		// Thống kê điểm danh
		Task<int> GetTotalAttendanceRecordsAsync(); // Tổng số bản ghi điểm danh
		Task<int> GetAbsentRecordsAsync(); // Tổng số lượt vắng mặt
		Task<int> GetLateRecordsAsync(); // Tổng số lượt đi trễ

		// Thống kê thông báo
		Task<int> GetTotalNotificationsAsync(); // Tổng số thông báo đã gửi
		Task<int> GetUnreadNotificationsAsync(int userId); // Số thông báo chưa đọc của người dùng

		Task<byte[]> ExportDataToExcelAsync<T>(string sheetName, IEnumerable<T> data, Dictionary<string, string> columnMappings = null);
	}
}
