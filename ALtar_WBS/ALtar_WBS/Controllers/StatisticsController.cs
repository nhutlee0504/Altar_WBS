using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALtar_WBS.Interface;
using static ALtar_WBS.Service.StatisticsService;

namespace ALtar_WBS.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StatisticsController : ControllerBase
	{
		private readonly IStatisticsService _statisticsService;

		public StatisticsController(IStatisticsService statisticsService)
		{
			_statisticsService = statisticsService;
		}

		[HttpGet("students/total")]
		public async Task<IActionResult> GetTotalStudents(bool exportExcel = false)
		{
			var totalStudents = await _statisticsService.GetTotalStudentsAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Tổng Số Học Viên", Value = totalStudents }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Lượng" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Total Students", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalStudents.xlsx");
			}

			return Ok(totalStudents);
		}

		[HttpGet("students/active")]
		public async Task<IActionResult> GetActiveStudents(bool exportExcel = false)
		{
			var activeStudents = await _statisticsService.GetActiveStudentsAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Số Học Viên Đang Hoạt Động", Value = activeStudents }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Lượng" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Active Students", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveStudents.xlsx");
			}

			return Ok(activeStudents);
		}

		[HttpGet("students/graduated")]
		public async Task<IActionResult> GetGraduatedStudents(bool exportExcel = false)
		{
			var graduatedStudents = await _statisticsService.GetGraduatedStudentsAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Số Học Viên Đã Tốt Nghiệp", Value = graduatedStudents }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Lượng" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Graduated Students", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GraduatedStudents.xlsx");
			}

			return Ok(graduatedStudents);
		}

		[HttpGet("teachers/total")]
		public async Task<IActionResult> GetTotalTeachers(bool exportExcel = false)
		{
			var totalTeachers = await _statisticsService.GetTotalTeachersAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Tổng Số Giáo Viên", Value = totalTeachers }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Lượng" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Total Teachers", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalTeachers.xlsx");
			}

			return Ok(totalTeachers);
		}

		[HttpGet("teachers/active")]
		public async Task<IActionResult> GetActiveTeachers(bool exportExcel = false)
		{
			var activeTeachers = await _statisticsService.GetActiveTeachersAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Số Giáo Viên Đang Hoạt Động", Value = activeTeachers }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Lượng" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Active Teachers", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ActiveTeachers.xlsx");
			}

			return Ok(activeTeachers);
		}

		[HttpGet("courses/total")]
		public async Task<IActionResult> GetTotalCourses(bool exportExcel = false)
		{
			var totalCourses = await _statisticsService.GetTotalCoursesAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Tổng Số Khóa Học", Value = totalCourses }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Lượng" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Total Courses", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalCourses.xlsx");
			}

			return Ok(totalCourses);
		}

		[HttpGet("courses/revenue/total")]
		public async Task<IActionResult> GetTotalCourseRevenue(bool exportExcel = false)
		{
			var totalRevenue = await _statisticsService.GetTotalCourseRevenueAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Tổng Doanh Thu Khóa Học", Value = totalRevenue }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Tiền" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Total Course Revenue", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalCourseRevenue.xlsx");
			}

			return Ok(totalRevenue);
		}

		[HttpGet("revenue/total")]
		public async Task<IActionResult> GetTotalRevenue(bool exportExcel = false)
		{
			var totalRevenue = await _statisticsService.GetTotalRevenueAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Tổng Doanh Thu", Value = totalRevenue }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Tiền" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Total Revenue", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TotalRevenue.xlsx");
			}

			return Ok(totalRevenue);
		}

		[HttpGet("profit/net")]
		public async Task<IActionResult> GetNetProfit(bool exportExcel = false)
		{
			var netProfit = await _statisticsService.GetNetProfitAsync();
			if (exportExcel)
			{
				var data = new List<ExportDataModel>
				{
					new ExportDataModel { Description = "Lợi Nhuận Ròng", Value = netProfit }
				};

				var columnMappings = new Dictionary<string, string>
				{
					{ "Description", "Loại Thống Kê" },
					{ "Value", "Số Tiền" }
				};

				var fileBytes = await _statisticsService.ExportDataToExcelAsync("Net Profit", data, columnMappings);
				return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NetProfit.xlsx");
			}

			return Ok(netProfit);
		}
	}
}
