using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var teacher = await _serviceTeacher.AddTeacher(teacherDto, profileImage);
                return Ok(teacher); // Nếu thành công, trả về kết quả với status 200 OK
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Nếu là InvalidOperationException, trả về lỗi đơn giản
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông báo lỗi đơn giản
            }
        }

        // Cập nhật thông tin giảng viên
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromForm] TeacherDto teacherDto, IFormFile profileImage)
        {
            try
            {
                var updatedTeacher = await _serviceTeacher.UpdateTeacher(id, teacherDto, profileImage);
                return Ok(updatedTeacher); // Nếu thành công, trả về kết quả với status 200 OK
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Nếu là InvalidOperationException, trả về lỗi đơn giản
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông báo lỗi đơn giản
            }
        }

        // Xóa giảng viên theo ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var result = await _serviceTeacher.DeleteTeacher(id);
                return Ok(result); // Thông báo thành công
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Nếu là InvalidOperationException, trả về lỗi đơn giản
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông báo lỗi đơn giản
            }
        }

        // Lấy danh sách tất cả giảng viên
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _serviceTeacher.GetAllTeachers();
                return Ok(teachers); // Trả về danh sách giảng viên với status 200 OK
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Nếu là InvalidOperationException, trả về lỗi đơn giản
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông báo lỗi đơn giản
            }
        }

        // Lấy thông tin giảng viên theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _serviceTeacher.GetTeacherById(id);
                return Ok(teacher); // Trả về thông tin giảng viên với status 200 OK
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Nếu là InvalidOperationException, trả về lỗi đơn giản
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông báo lỗi đơn giản
            }
        }

        // Kiểm tra sự tồn tại của giảng viên theo ID
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> TeacherExists(int id)
        {
            try
            {
                var exists = await _serviceTeacher.TeacherExists(id);
                return Ok(exists); // Trả về kết quả kiểm tra sự tồn tại của giảng viên với status 200 OK
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Nếu là InvalidOperationException, trả về lỗi đơn giản
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông báo lỗi đơn giản
            }
        }
    }
}
