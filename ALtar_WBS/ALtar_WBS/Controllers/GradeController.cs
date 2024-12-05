using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using ALtar_WBS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly InterfaceGrade _serviceGrade;

        public GradeController(InterfaceGrade serviceGrade)
        {
            _serviceGrade = serviceGrade;
        }

        [HttpPost]
        public async Task<IActionResult> AddGradeAsync([FromBody] GradeDto gradeDto)
        {
            if (gradeDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var grade = await _serviceGrade.AddGradeAsync(gradeDto.StudentID, gradeDto.CourseID, gradeDto.GradeValue, gradeDto.Remarks);
                return Ok(grade);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGradeAsync(int id, [FromBody] GradeDto gradeDto)
        {
            if (gradeDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var updatedGrade = await _serviceGrade.UpdateGradeAsync(id, gradeDto.GradeValue, gradeDto.Remarks);
                return Ok(updatedGrade);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-course/{courseId}")]
        public async Task<IActionResult> GetGradesByCourseAsync(int courseId)
        {
            try
            {
                var grades = await _serviceGrade.GetGradesByCourseAsync(courseId);
                return Ok(grades);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetGradesByStudentAsync(int studentId)
        {
            try
            {
                var grades = await _serviceGrade.GetGradesByStudentAsync(studentId);
                return Ok(grades);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeByIdAsync(int id)
        {
            try
            {
                var grade = await _serviceGrade.GetGradeByIdAsync(id);
                if (grade == null)
                {
                    return NotFound("Không tìm thấy điểm.");
                }
                return Ok(grade);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveGradeAsync(int id)
        {
            try
            {
                await _serviceGrade.RemoveGradeAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
