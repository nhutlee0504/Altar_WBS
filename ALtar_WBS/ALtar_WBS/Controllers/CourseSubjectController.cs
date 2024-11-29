using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSubjectController : ControllerBase
    {
        private readonly InterfaceCourseSubject _courseSubjectService;

        public CourseSubjectController(InterfaceCourseSubject courseSubjectService)
        {
            _courseSubjectService = courseSubjectService;
        }

        // Add a new Course-Subject relationship
        [HttpPost]
        public async Task<IActionResult> AddCourseSubject(int courseId, int subjectId)
        {
            try
            {
                var courseSubject = await _courseSubjectService.AddCourseSubjectAsync(courseId, subjectId);
                return Ok(courseSubject);
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

        // Check if a Course-Subject relationship exists
        [HttpGet("exists")]
        public async Task<IActionResult> CourseSubjectExists(int courseId, int subjectId)
        {
            try
            {
                var exists = await _courseSubjectService.CourseSubjectExistsAsync(courseId, subjectId);
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

        // Delete a Course-Subject relationship by ID
        [HttpDelete("{csId}")]
        public async Task<IActionResult> DeleteCourseSubject(int csId)
        {
            try
            {
                var result = await _courseSubjectService.DeleteCourseSubjectAsync(csId);
                if (result)
                    return Ok(new { message = "Deleted successfully." });

                return NotFound(new { message = "Course-Subject relationship not found." });
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

        // Get all Courses by a specific Subject ID
        [HttpGet("courses-by-subject/{subjectId}")]
        public async Task<IActionResult> GetCoursesBySubject(int subjectId)
        {
            try
            {
                var courses = await _courseSubjectService.GetCoursesBySubjectAsync(subjectId);
                return Ok(courses);
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

        // Get all Subjects by a specific Course ID
        [HttpGet("subjects-by-course/{courseId}")]
        public async Task<IActionResult> GetSubjectsByCourse(int courseId)
        {
            try
            {
                var subjects = await _courseSubjectService.GetSubjectsByCourseAsync(courseId);
                return Ok(subjects);
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

        // Update a Course-Subject relationship
        [HttpPut("{csId}")]
        public async Task<IActionResult> UpdateCourseSubject(int csId, int courseId, int subjectId)
        {
            try
            {
                var updatedCourseSubject = await _courseSubjectService.UpdateCourseSubjectAsync(csId, courseId, subjectId);
                return Ok(updatedCourseSubject);
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
