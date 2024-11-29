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
                return Ok(student);  // Trả về sinh viên đã thêm
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi chung
            }
        }

        // Lấy danh sách sinh viên
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudents();
                return Ok(students);  // Trả về danh sách sinh viên
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi chung
            }
        }

        // Lấy thông tin sinh viên theo ID
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentService.GetStudentById(studentId);
                return Ok(student);  // Trả về thông tin sinh viên
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi chung
            }
        }

        // Cập nhật thông tin sinh viên
        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromForm] StudentDto studentDto, IFormFile profileImage)
        {
            try
            {
                var updatedStudent = await _studentService.UpdateStudent(studentId, studentDto, profileImage);
                return Ok(updatedStudent);  // Trả về sinh viên đã cập nhật
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi chung
            }
        }

        // Xóa sinh viên
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            try
            {
                var success = await _studentService.DeleteStudent(studentId);
                return Ok(success);  // Trả về thông báo xóa thành công
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi chung
            }
        }

        // Kiểm tra sinh viên có tồn tại không
        [HttpGet("exists/{studentId}")]
        public async Task<IActionResult> StudentExists(int studentId)
        {
            try
            {
                var exists = await _studentService.StudentExists(studentId);
                return Ok(new { exists });  // Trả về kết quả kiểm tra sinh viên
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Trả về thông báo lỗi chung
            }
        }
    }
}
