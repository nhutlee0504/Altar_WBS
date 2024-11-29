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
        public async Task<IActionResult> AddCategory([FromForm] string categoryName)
        {
            try
            {
                var category = await _subjectCategoryService.AddCategoryAsync(categoryName);
                return Ok(category); // Trả về kết quả thêm thành công
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi nếu có
            }
        }

        // Cập nhật loại môn học
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] string categoryName)
        {
            try
            {
                var updatedCategory = await _subjectCategoryService.UpdateCategoryAsync(id, categoryName);
                return Ok(updatedCategory); // Trả về kết quả sau khi cập nhật thành công
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi nếu có
            }
        }

        // Xóa loại môn học
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _subjectCategoryService.DeleteCategoryAsync(id);
                return NoContent(); // Trả về trạng thái NoContent nếu xóa thành công
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi nếu có
            }
        }

        // Lấy danh sách loại môn học
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _subjectCategoryService.GetAllCategoriesAsync();
                return Ok(categories); // Trả về danh sách các loại môn học
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi nếu có
            }
        }

        // Lấy thông tin loại môn học theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _subjectCategoryService.GetCategoryByIdAsync(id);
                return Ok(category); // Trả về thông tin danh mục môn học nếu tìm thấy
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi nếu có
            }
        }

        // Kiểm tra loại môn học có tồn tại không
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> CategoryExists(int id)
        {
            try
            {
                var exists = await _subjectCategoryService.CategoryExistsAsync(id);
                return Ok(exists); // Trả về kết quả tồn tại hoặc không
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về lỗi nếu có
            }
        }
    }
}
