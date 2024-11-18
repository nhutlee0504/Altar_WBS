using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly InterfaceTeacher _serviceTeacher;

        public TeacherController(InterfaceTeacher serviceTeacher)
        {
            _serviceTeacher = serviceTeacher;
        }

        // Thêm giảng viên mới
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher([FromForm] TeacherDto teacherDto, IFormFile profileImage)
        {
            var teacher = await _serviceTeacher.AddTeacher(teacherDto, profileImage);
            if (teacher == null) return BadRequest("Không thể thêm giảng viên.");
            return Ok(teacher);
        }

        // Cập nhật thông tin giảng viên
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromForm] TeacherDto teacherDto, IFormFile profileImage)
        {
            var updatedTeacher = await _serviceTeacher.UpdateTeacher(id, teacherDto, profileImage);
            if (updatedTeacher == null) return NotFound("Giảng viên không tồn tại.");
            return Ok(updatedTeacher);
        }

        // Xóa giảng viên theo ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var result = await _serviceTeacher.DeleteTeacher(id);
            if (!result) return NotFound("Giảng viên không tồn tại.");
            return Ok("Xóa giảng viên thành công.");
        }

        // Lấy danh sách tất cả giảng viên
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _serviceTeacher.GetAllTeachers();
            return Ok(teachers);
        }

        // Lấy thông tin giảng viên theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _serviceTeacher.GetTeacherById(id);
            if (teacher == null) return NotFound("Giảng viên không tồn tại.");
            return Ok(teacher);
        }

        // Kiểm tra sự tồn tại của giảng viên theo ID
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> TeacherExists(int id)
        {
            var exists = await _serviceTeacher.TeacherExists(id);
            return Ok(exists);
        }
    }
}
