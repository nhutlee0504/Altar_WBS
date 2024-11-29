using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSalaryController : ControllerBase
    {
        private readonly InterfaceTeacherSalary _salaryService;

        public TeacherSalaryController(InterfaceTeacherSalary salaryService)
        {
            _salaryService = salaryService;
        }

        // Add salary
        [HttpPost]
        public async Task<IActionResult> AddSalary([FromForm] TeacherSalaryDto salaryDto)
        {
            try
            {
                var result = await _salaryService.AddSalary(salaryDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handle all other exceptions
            }
        }

        // Update salary
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalary(int id, [FromForm] TeacherSalaryDto salaryDto)
        {
            try
            {
                var result = await _salaryService.UpdateSalary(id, salaryDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handle all other exceptions
            }
        }

        // Delete salary
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            try
            {
                await _salaryService.DeleteSalary(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handle all other exceptions
            }
        }

        // Get all salaries
        [HttpGet]
        public async Task<IActionResult> GetAllSalaries()
        {
            try
            {
                var result = await _salaryService.GetAllSalaries();
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handle all other exceptions
            }
        }

        // Get salary by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaryById(int id)
        {
            try
            {
                var result = await _salaryService.GetSalaryById(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Handle InvalidOperationException
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Handle all other exceptions
            }
        }
    }
}
