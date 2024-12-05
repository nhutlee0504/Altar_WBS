using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTeacherController : ControllerBase
    {
        private readonly InterfaceClassTeacher _service;

        public ClassTeacherController(InterfaceClassTeacher service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddClassTeacherAsync(int classId, int teacherId)
        {
            try
            {
                await _service.AddClassTeacherAsync(classId, teacherId);
                return Ok(new { Message = "Class teacher added successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("get-classes-by-teacher/{teacherId}")]
        public async Task<IActionResult> GetClassesByTeacherAsync(int teacherId)
        {
            try
            {
                var classes = await _service.GetClassesByTeacherAsync(teacherId);
                return Ok(new { Classes = classes });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("get-teachers-by-class/{classId}")]
        public async Task<IActionResult> GetTeachersByClassAsync(int classId)
        {
            try
            {
                var teachers = await _service.GetTeachersByClassAsync(classId);
                return Ok(new { Teachers = teachers });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("is-teacher-assigned/{classId}/{teacherId}")]
        public async Task<IActionResult> IsTeacherAssignedToClassAsync(int classId, int teacherId)
        {
            try
            {
                var isAssigned = await _service.IsTeacherAssignedToClassAsync(classId, teacherId);
                return Ok(new { IsAssigned = isAssigned });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveClassTeacherAsync(int classId, int teacherId)
        {
            try
            {
                await _service.RemoveClassTeacherAsync(classId, teacherId);
                return Ok(new { Message = "Class teacher removed successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
