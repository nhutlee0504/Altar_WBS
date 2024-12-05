using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly InterfaceCourse _courseService;

        public CourseController(InterfaceCourse courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm] CourseDto course)
        {
            try
            {
                var addedCourse = await _courseService.AddCourseAsync(course);
                return Ok(addedCourse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromForm] CourseDto course)
        {
            try
            {
                var updatedCourse = await _courseService.UpdateCourseAsync(id, course);
                return Ok(updatedCourse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var result = await _courseService.DeleteCourseAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                return Ok(courses);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                return Ok(course);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("duration/{duration}")]
        public async Task<IActionResult> GetCoursesByDuration(int duration)
        {
            try
            {
                var courses = await _courseService.GetCoursesByDurationAsync(duration);
                return Ok(courses);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fee-range")]
        public async Task<IActionResult> GetCoursesByFeeRange(decimal minFee, decimal maxFee)
        {
            try
            {
                var courses = await _courseService.GetCoursesByFeeRangeAsync(minFee, maxFee);
                return Ok(courses);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ongoing")]
        public async Task<IActionResult> GetOngoingCourses()
        {
            try
            {
                var courses = await _courseService.GetOngoingCoursesAsync();
                return Ok(courses);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
