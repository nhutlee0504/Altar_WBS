using ALtar_WBS.Dto;
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
        public async Task<IActionResult> AddSubject([FromForm] SubjectDto subject)
        {
            try
            {
                var addedSubject = await _subjectService.AddSubjectAsync(subject);
                return Ok(addedSubject); // Return Ok with the result
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Return BadRequest with the error message
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return internal server error
            }
        }

        // Cập nhật môn học
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromForm] SubjectDto subject)
        {
            try
            {
                var updatedSubject = await _subjectService.UpdateSubjectAsync(id, subject);
                return Ok(updatedSubject); // Return Ok with the updated subject
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return NotFound with the error message
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return internal server error
            }
        }

        // Xóa môn học
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var result = await _subjectService.DeleteSubjectAsync(id);
                return NoContent(); // Return NoContent for successful deletion
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return NotFound with the error message
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return internal server error
            }
        }

        // Lấy danh sách môn học
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetAllSubjectsAsync();
                return Ok(subjects); // Return Ok with the list of subjects
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return internal server error
            }
        }

        // Lấy thông tin môn học theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);
                return Ok(subject); // Return Ok with the subject
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return NotFound with the error message
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return internal server error
            }
        }

        // Kiểm tra môn học có tồn tại không
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> SubjectExists(int id)
        {
            try
            {
                var exists = await _subjectService.SubjectExistsAsync(id);
                return Ok(exists); // Return Ok with existence status
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Return internal server error
            }
        }
    }
}
