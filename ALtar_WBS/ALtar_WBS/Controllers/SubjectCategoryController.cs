using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectCategoryController : ControllerBase
    {
        private readonly ISubjectCategory _subjectCategoryService;

        public SubjectCategoryController(ISubjectCategory subjectCategoryService)
        {
            _subjectCategoryService = subjectCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] string categoryName)
        {
            try
            {
                var category = await _subjectCategoryService.AddCategoryAsync(categoryName);
                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] string categoryName)
        {
            try
            {
                var updatedCategory = await _subjectCategoryService.UpdateCategoryAsync(id, categoryName);
                return Ok(updatedCategory);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _subjectCategoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _subjectCategoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _subjectCategoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CategoryExists(int id)
        {
            try
            {
                var exists = await _subjectCategoryService.CategoryExistsAsync(id);
                return Ok(exists);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
