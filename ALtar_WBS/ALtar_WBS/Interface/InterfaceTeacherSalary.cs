using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceTeacherSalary
	{
		public Task<TeacherSalary> AddSalary(TeacherSalaryDto salaryDto);
		public Task<TeacherSalary> UpdateSalary(int salaryId, TeacherSalaryDto salaryDto);
		public Task<bool> DeleteSalary(int salaryId);
		public Task<IEnumerable<TeacherSalary>> GetAllSalaries();
		public Task<TeacherSalary> GetSalaryById(int salaryId);
		public Task<bool> SalaryExists(int salaryId);
	}
}
