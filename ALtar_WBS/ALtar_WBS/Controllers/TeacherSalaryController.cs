using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeacherSalaryController : ControllerBase
	{
		private readonly InterfaceTeacherSalary _salaryService;

		public TeacherSalaryController(InterfaceTeacherSalary salaryService)
		{
			_salaryService = salaryService;
		}

		[HttpPost]
		public async Task<IActionResult> AddSalary(TeacherSalaryDto salaryDto)
		{
			var result = await _salaryService.AddSalary(salaryDto);
			return CreatedAtAction(nameof(GetSalaryById), new { id = result.SalaryID }, result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSalary(int id, TeacherSalaryDto salaryDto)
		{
			var result = await _salaryService.UpdateSalary(id, salaryDto);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSalary(int id)
		{
			await _salaryService.DeleteSalary(id);
			return NoContent();
		}

		[HttpGet]
		public async Task<IActionResult> GetAllSalaries()
		{
			var result = await _salaryService.GetAllSalaries();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSalaryById(int id)
		{
			var result = await _salaryService.GetSalaryById(id);
			return Ok(result);
		}
	}
}
