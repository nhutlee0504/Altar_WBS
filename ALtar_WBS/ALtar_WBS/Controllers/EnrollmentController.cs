using ALtar_WBS.Interface;
using ALtar_WBS.Dto;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EnrollmentController : ControllerBase
	{
		private readonly InterfaceEnrollment _service;

		public EnrollmentController(InterfaceEnrollment service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<ActionResult<Enrollment>> AddEnrollment([FromBody] EnrollmentDto enrollmentDto)
		{
			try
			{
				var newEnrollment = await _service.AddEnrollmentAsync(enrollmentDto);
				return CreatedAtAction(nameof(GetEnrollmentById), new { id = newEnrollment.EnrollmentID }, newEnrollment);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteEnrollment(int id)
		{
			try
			{
				var deletedEnrollment = await _service.DeleteEnrollmentAsync(id);
				return Ok(deletedEnrollment);
			}
			catch (Exception ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Enrollment>>> GetAllEnrollments()
		{
			try
			{
				var enrollments = await _service.GetAllEnrollmentsAsync();
				return Ok(enrollments);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = ex.Message });
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Enrollment>> GetEnrollmentById(int id)
		{
			try
			{
				var enrollment = await _service.GetEnrollmentByIdAsync(id);
				if (enrollment == null)
				{
					return NotFound(new { message = "Enrollment not found." });
				}
				return Ok(enrollment);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Enrollment>> UpdateEnrollment(int id, [FromBody] EnrollmentDto enrollmentDto)
		{
			try
			{
				var updatedEnrollment = await _service.UpdateEnrollmentAsync(id, enrollmentDto);
				return Ok(updatedEnrollment);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
