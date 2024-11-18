using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceTeacherSalary : InterfaceTeacherSalary
	{
		private readonly ApplicationDbContext _context;

		public ServiceTeacherSalary(ApplicationDbContext context)
		{
			_context = context;
		}

		// Thêm thông tin lương giảng viên
		public async Task<TeacherSalary> AddSalary(TeacherSalaryDto salaryDto)
		{
			var newSalary = new TeacherSalary
			{
				TeacherID = salaryDto.TeacherID,
				amount = salaryDto.amount,
				PaymentDate = salaryDto.PaymentDate,
				PaymentType = salaryDto.PaymentType,
			};

			await _context.teacherSalaries.AddAsync(newSalary);
			await _context.SaveChangesAsync();

			return newSalary;
		}

		// Cập nhật thông tin lương
		public async Task<TeacherSalary> UpdateSalary(int salaryId, TeacherSalaryDto salaryDto)
		{
			var existingSalary = await _context.teacherSalaries.FindAsync(salaryId);

			if (existingSalary == null)
				throw new KeyNotFoundException("Lương không tồn tại.");

			existingSalary.TeacherID = salaryDto.TeacherID;
			existingSalary.amount = salaryDto.amount;
			existingSalary.PaymentDate = salaryDto.PaymentDate;
			existingSalary.PaymentType = salaryDto.PaymentType;

			_context.teacherSalaries.Update(existingSalary);
			await _context.SaveChangesAsync();

			return existingSalary;
		}

		// Xóa thông tin lương
		public async Task<bool> DeleteSalary(int salaryId)
		{
			var salary = await _context.teacherSalaries.FindAsync(salaryId);

			if (salary == null)
				throw new KeyNotFoundException("Lương không tồn tại.");

			_context.teacherSalaries.Remove(salary);
			await _context.SaveChangesAsync();

			return true;
		}

		// Lấy danh sách lương giảng viên
		public async Task<IEnumerable<TeacherSalary>> GetAllSalaries()
		{
			return await _context.teacherSalaries.ToListAsync();
		}

		// Lấy thông tin lương theo ID
		public async Task<TeacherSalary> GetSalaryById(int salaryId)
		{
			var salary = await _context.teacherSalaries.FindAsync(salaryId);

			if (salary == null)
				throw new KeyNotFoundException("Lương không tồn tại.");

			return salary;
		}

		// Kiểm tra lương có tồn tại không
		public async Task<bool> SalaryExists(int salaryId)
		{
			return await _context.teacherSalaries.AnyAsync(s => s.SalaryID == salaryId);
		}
	}
}
