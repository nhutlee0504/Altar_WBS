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

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromForm] StudentDto studentDto, IFormFile profileImage)
        {
            try
            {
                var student = await _studentService.AddStudent(studentDto, profileImage);
                return Ok(student);
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

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudents();
                return Ok(students);
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

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentService.GetStudentById(studentId);
                return Ok(student);
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

        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromForm] StudentDto studentDto, IFormFile profileImage)
        {
            try
            {
                var updatedStudent = await _studentService.UpdateStudent(studentId, studentDto, profileImage);
                return Ok(updatedStudent);
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

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            try
            {
                var success = await _studentService.DeleteStudent(studentId);
                return Ok(success);
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

        [HttpGet("exists/{studentId}")]
        public async Task<IActionResult> StudentExists(int studentId)
        {
            try
            {
                var exists = await _studentService.StudentExists(studentId);
                return Ok(new { exists });
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
