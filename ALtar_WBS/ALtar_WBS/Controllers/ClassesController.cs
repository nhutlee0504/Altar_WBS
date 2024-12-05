using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ALtar_WBS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassesController : ControllerBase
	{
		private readonly InterfaceClasses _classesService;

		public ClassesController(InterfaceClasses classesService)
		{
			_classesService = classesService;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllClasses()
		{
			try
			{
				var classes = await _classesService.GetAllClassesAsync();
				return Ok(classes);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("course/{courseId}")]
		public async Task<ActionResult> GetClassesByCourseId(int courseId)
		{
			try
			{
				var classes = await _classesService.GetClassesByCourseIdAsync(courseId);
				return Ok(classes);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{classId}")]
		public async Task<ActionResult> GetClassById(int classId)
		{
			try
			{
				var classEntity = await _classesService.GetClassByIdAsync(classId);
				return Ok(classEntity);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> CreateClass(int courseId)
		{
			try
			{
				var newClass = await _classesService.CreateClassAsync(courseId);
				return Ok(newClass);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{classId}")]
		public async Task<ActionResult> UpdateClass(int classId, int courseId)
		{
			try
			{
				var updatedClass = await _classesService.UpdateClassAsync(classId, courseId);
				return Ok(updatedClass);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{classId}")]
		public async Task<ActionResult> DeleteClass(int classId)
		{
			try
			{
				var success = await _classesService.DeleteClassAsync(classId);
				if (success)
				{
					return Ok("Class deleted successfully.");
				}
				else
				{
					return BadRequest("Class not found.");
				}
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
