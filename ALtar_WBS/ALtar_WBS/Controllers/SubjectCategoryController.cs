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

		// Thêm loại môn học
		[HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] string categoryName)
		{
			if (string.IsNullOrWhiteSpace(categoryName))
				return BadRequest("Category name cannot be empty.");

			var category = await _subjectCategoryService.AddCategoryAsync(categoryName);
			return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryID }, category);
		}

		// Cập nhật loại môn học
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] string categoryName)
		{
			if (string.IsNullOrWhiteSpace(categoryName))
				return BadRequest("Category name cannot be empty.");

			var updatedCategory = await _subjectCategoryService.UpdateCategoryAsync(id, categoryName);
			if (updatedCategory == null)
				return NotFound($"Category with ID {id} not found.");

			return Ok(updatedCategory);
		}

		// Xóa loại môn học
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await _subjectCategoryService.DeleteCategoryAsync(id);
			if (!result)
				return NotFound($"Category with ID {id} not found.");

			return NoContent();
		}

		// Lấy danh sách loại môn học
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SubjectCategories>>> GetAllCategories()
		{
			var categories = await _subjectCategoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}

		// Lấy thông tin loại môn học theo ID
		[HttpGet("{id}")]
		public async Task<ActionResult<SubjectCategories>> GetCategoryById(int id)
		{
			var category = await _subjectCategoryService.GetCategoryByIdAsync(id);
			if (category == null)
				return NotFound($"Category with ID {id} not found.");

			return Ok(category);
		}

		// Kiểm tra loại môn học có tồn tại không
		[HttpGet("exists/{id}")]
		public async Task<IActionResult> CategoryExists(int id)
		{
			var exists = await _subjectCategoryService.CategoryExistsAsync(id);
			return Ok(new { Exists = exists });
		}
	}
}
