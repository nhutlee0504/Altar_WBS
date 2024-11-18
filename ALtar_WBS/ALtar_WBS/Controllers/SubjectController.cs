using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private readonly InterfaceSubject _subjectService;

		public SubjectController(InterfaceSubject subjectService)
		{
			_subjectService = subjectService;
		}

		// Thêm môn học
		[HttpPost]
		public async Task<IActionResult> AddSubject([FromBody] Subjects subject)
		{
			if (subject == null)
				return BadRequest("Invalid subject data.");

			var addedSubject = await _subjectService.AddSubjectAsync(subject);
			return CreatedAtAction(nameof(GetSubjectById), new { id = addedSubject.SubjectID }, addedSubject);
		}

		// Cập nhật môn học
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSubject(int id, [FromBody] Subjects subject)
		{
			if (subject == null || id != subject.SubjectID)
				return BadRequest("Invalid subject data.");

			var updatedSubject = await _subjectService.UpdateSubjectAsync(id, subject);
			if (updatedSubject == null)
				return NotFound($"Subject with ID {id} not found.");

			return Ok(updatedSubject);
		}

		// Xóa môn học
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			var result = await _subjectService.DeleteSubjectAsync(id);
			if (!result)
				return NotFound($"Subject with ID {id} not found.");

			return NoContent();
		}

		// Lấy danh sách môn học
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Subjects>>> GetAllSubjects()
		{
			var subjects = await _subjectService.GetAllSubjectsAsync();
			return Ok(subjects);
		}

		// Lấy thông tin môn học theo ID
		[HttpGet("{id}")]
		public async Task<ActionResult<Subjects>> GetSubjectById(int id)
		{
			var subject = await _subjectService.GetSubjectByIdAsync(id);
			if (subject == null)
				return NotFound($"Subject with ID {id} not found.");

			return Ok(subject);
		}

		// Kiểm tra môn học có tồn tại không
		[HttpGet("exists/{id}")]
		public async Task<IActionResult> SubjectExists(int id)
		{
			var exists = await _subjectService.SubjectExistsAsync(id);
			return Ok(new { Exists = exists });
		}
	}
}
