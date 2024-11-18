using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly InterfaceStudent _studentService;

		public StudentsController(InterfaceStudent studentService)
		{
			_studentService = studentService;
		}

		// Thêm sinh viên
		[HttpPost]
		public async Task<IActionResult> AddStudent([FromForm] StudentDto studentDto, IFormFile profileImage)
		{
			try
			{
				var student = await _studentService.AddStudent(studentDto, profileImage);
				return CreatedAtAction(nameof(GetStudentById), new { studentId = student.UserID }, student);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// Lấy danh sách sinh viên
		[HttpGet]
		public async Task<IActionResult> GetAllStudents()
		{
			var students = await _studentService.GetAllStudents();
			return Ok(students);
		}

		// Lấy thông tin sinh viên theo ID
		[HttpGet("{studentId}")]
		public async Task<IActionResult> GetStudentById(int studentId)
		{
			var student = await _studentService.GetStudentById(studentId);
			if (student == null)
			{
				return NotFound(new { message = "Student not found." });
			}
			return Ok(student);
		}

		// Cập nhật thông tin sinh viên
		[HttpPut("{studentId}")]
		public async Task<IActionResult> UpdateStudent(int studentId, [FromForm] StudentDto studentDto, IFormFile profileImage)
		{
			try
			{
				var updatedStudent = await _studentService.UpdateStudent(studentId, studentDto, profileImage);
				return Ok(updatedStudent);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		// Xóa sinh viên
		[HttpDelete("{studentId}")]
		public async Task<IActionResult> DeleteStudent(int studentId)
		{
			var success = await _studentService.DeleteStudent(studentId);
			if (!success)
			{
				return NotFound(new { message = "Student not found or could not be deleted." });
			}
			return NoContent();
		}

		// Kiểm tra sinh viên có tồn tại không
		[HttpGet("exists/{studentId}")]
		public async Task<IActionResult> StudentExists(int studentId)
		{
			var exists = await _studentService.StudentExists(studentId);
			return Ok(new { exists });
		}
	}
}
