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

        [HttpPost]
        public async Task<IActionResult> AddSubject([FromForm] SubjectDto subject)
        {
            try
            {
                var addedSubject = await _subjectService.AddSubjectAsync(subject);
                return Ok(addedSubject);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromForm] SubjectDto subject)
        {
            try
            {
                var updatedSubject = await _subjectService.UpdateSubjectAsync(id, subject);
                return Ok(updatedSubject);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var result = await _subjectService.DeleteSubjectAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetAllSubjectsAsync();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            try
            {
                var subject = await _subjectService.GetSubjectByIdAsync(id);
                return Ok(subject);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> SubjectExists(int id)
        {
            try
            {
                var exists = await _subjectService.SubjectExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
