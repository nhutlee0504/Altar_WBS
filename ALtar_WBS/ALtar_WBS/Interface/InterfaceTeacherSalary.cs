using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceTeacherSalary
	{
		public Task<TeacherSalary> AddSalary(TeacherSalaryDto salaryDto); // Thêm thông tin lương giảng viên
		public Task<TeacherSalary> UpdateSalary(int salaryId, TeacherSalaryDto salaryDto); // Cập nhật thông tin lương
		public Task<bool> DeleteSalary(int salaryId); // Xóa thông tin lương
		public Task<IEnumerable<TeacherSalary>> GetAllSalaries(); // Lấy danh sách lương giảng viên
		public Task<TeacherSalary> GetSalaryById(int salaryId); // Lấy thông tin lương theo ID
		public Task<bool> SalaryExists(int salaryId); // Kiểm tra lương có tồn tại không
	}
}
