using System;
using System.Linq;
using System.Threading.Tasks;
using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ALtar_WBS.Service
{
	public class StatisticsService : IStatisticsService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public StatisticsService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<int> GetTotalStudentsAsync()
		{
			return await _context.students.CountAsync();
		}

		public async Task<int> GetActiveStudentsAsync()
		{
			return await _context.students
				.Where(s => s.Enrollments.Any(e => e.status == "Active"))
				.CountAsync();
		}

		public async Task<int> GetGraduatedStudentsAsync()
		{
			return await _context.students
				.Where(s => s.Enrollments.All(e => e.status == "Graduated"))
				.CountAsync();
		}

		public async Task<decimal> GetTotalStudentPaymentsAsync()
		{
			return await _context.payments.SumAsync(p => p.Amount);
		}

		public async Task<int> GetTotalTeachersAsync()
		{
			return await _context.teachers.CountAsync();
		}

		public async Task<int> GetActiveTeachersAsync()
		{
			return await _context.teachers
				.Where(t => t.ClassTeachers.Any())
				.CountAsync();
		}

		public async Task<decimal> GetTotalTeacherSalariesAsync()
		{
			return await _context.teacherSalaries.SumAsync(s => s.amount);
		}

		public async Task<int> GetTotalCoursesAsync()
		{
			return await _context.courses.CountAsync();
		}

		public async Task<int> GetOngoingCoursesAsync()
		{
			var today = DateTime.Now;
			return await _context.courses
				.Where(c => c.StartDate <= today && c.EndDate >= today)
				.CountAsync();
		}

		public async Task<int> GetCompletedCoursesAsync()
		{
			return await _context.courses
				.Where(c => c.EndDate < DateTime.Now)
				.CountAsync();
		}

		public async Task<decimal> GetTotalCourseRevenueAsync()
		{
			return await _context.payments.SumAsync(p => p.Amount);
		}

		public async Task<int> GetTotalClassesAsync()
		{
			return await _context.classes.CountAsync();
		}

		public async Task<int> GetActiveClassesAsync()
		{
			return await _context.classes
				.Where(c => c.Course.EndDate >= DateTime.Now)
				.CountAsync();
		}

		public async Task<int> GetTotalEnrollmentsAsync()
		{
			return await _context.enrollments.CountAsync();
		}

		public async Task<decimal> GetTotalRevenueAsync()
		{
			return await _context.payments.SumAsync(p => p.Amount);
		}

		public async Task<decimal> GetTotalExpensesAsync()
		{
			return await _context.teacherSalaries.SumAsync(s => s.amount);
		}

		public async Task<decimal> GetNetProfitAsync()
		{
			var revenue = await GetTotalRevenueAsync();
			var expenses = await GetTotalExpensesAsync();
			return revenue - expenses;
		}

		public async Task<int> GetTotalAttendanceRecordsAsync()
		{
			return await _context.attendances.CountAsync();
		}

		public async Task<int> GetAbsentRecordsAsync()
		{
			return await _context.attendances
				.Where(a => a.Status == "Vắng mặt")
				.CountAsync();
		}

		public async Task<int> GetLateRecordsAsync()
		{
			return await _context.attendances
				.Where(a => a.Status == "Trễ")
				.CountAsync();
		}

		public async Task<int> GetTotalNotificationsAsync()
		{
			return await _context.notifications.CountAsync();
		}

		public async Task<int> GetUnreadNotificationsAsync(int userId)
		{
			return await _context.userNotifications
				.Where(un => un.UserID == userId)
				.CountAsync();
		}

		public async Task<byte[]> ExportDataToExcelAsync<T>(string sheetName, IEnumerable<T> data, Dictionary<string, string> columnMappings = null)
		{
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add(sheetName);

				if (data != null && data.Any())
				{
					var properties = typeof(T).GetProperties();

					for (int i = 0; i < properties.Length; i++)
					{
						var propertyName = properties[i].Name;

						worksheet.Cells[1, i + 1].Value = columnMappings != null && columnMappings.ContainsKey(propertyName)
							? columnMappings[propertyName]
							: propertyName;
					}

					worksheet.Cells[2, 1].LoadFromCollection(data, false);

					worksheet.Cells.AutoFitColumns();
				}
				else
				{
					worksheet.Cells[1, 1].Value = "No Data Available";
				}

				return package.GetAsByteArray();
			}
		}

		public class ExportDataModel
		{
			public string Description { get; set; }
			public object Value { get; set; }
		}
	}
}
