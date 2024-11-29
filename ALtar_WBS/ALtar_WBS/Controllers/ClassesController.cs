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

        // Lấy tất cả các lớp học
        [HttpGet]
        public async Task<ActionResult> GetAllClasses()
        {
            try
            {
                var classes = await _classesService.GetAllClassesAsync();
                return Ok(classes); // Trả về danh sách lớp học
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông điệp lỗi
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi server nếu có lỗi không xác định
            }
        }

        // Lấy tất cả lớp học theo CourseID
        [HttpGet("course/{courseId}")]
        public async Task<ActionResult> GetClassesByCourseId(int courseId)
        {
            try
            {
                var classes = await _classesService.GetClassesByCourseIdAsync(courseId);
                return Ok(classes); // Trả về lớp học của khóa học
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông điệp lỗi
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);// Trả về lỗi server nếu có lỗi không xác định
            }
        }

        // Lấy lớp học theo ClassID
        [HttpGet("{classId}")]
        public async Task<ActionResult> GetClassById(int classId)
        {
            try
            {
                var classEntity = await _classesService.GetClassByIdAsync(classId);
                return Ok(classEntity); // Trả về lớp học tìm được
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông điệp lỗi
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi server nếu có lỗi không xác định
            }
        }

        // Tạo mới một lớp học
        [HttpPost]
        public async Task<ActionResult> CreateClass(int courseId)
        {
            try
            {
                var newClass = await _classesService.CreateClassAsync(courseId);
                return Ok(newClass); // Trả về lớp học mới tạo
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông điệp lỗi
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi server nếu có lỗi không xác định
            }
        }

        // Cập nhật lớp học
        [HttpPut("{classId}")]
        public async Task<ActionResult> UpdateClass(int classId, int courseId)
        {
            try
            {
                var updatedClass = await _classesService.UpdateClassAsync(classId, courseId);
                return Ok(updatedClass); // Trả về lớp học đã cập nhật
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông điệp lỗi
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi server nếu có lỗi không xác định
            }
        }

        // Xóa lớp học
        [HttpDelete("{classId}")]
        public async Task<ActionResult> DeleteClass(int classId)
        {
            try
            {
                var success = await _classesService.DeleteClassAsync(classId);
                if (success)
                {
                    return Ok("Class deleted successfully."); // Trả về thông báo xóa thành công
                }
                else
                {
                    return BadRequest("Class not found."); // Trả về lỗi nếu lớp học không tồn tại
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi BadRequest với thông điệp lỗi
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi server nếu có lỗi không xác định
            }
        }
    }
}
