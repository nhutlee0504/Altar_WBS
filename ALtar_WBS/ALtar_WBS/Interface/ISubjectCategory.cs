using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface ISubjectCategory
	{
		public Task<SubjectCategories> AddCategoryAsync(string categoryName);  // Thêm loại môn học
		public Task<SubjectCategories> UpdateCategoryAsync(int categoryId, string categoryName);  // Cập nhật loại môn học
		public Task<bool> DeleteCategoryAsync(int categoryId);  // Xóa loại môn học
		public Task<IEnumerable<SubjectCategories>> GetAllCategoriesAsync();  // Lấy danh sách loại môn học
		public Task<SubjectCategories> GetCategoryByIdAsync(int categoryId);  // Lấy loại môn học theo ID
		public Task<bool> CategoryExistsAsync(int categoryId);  // Kiểm tra loại môn học có tồn tại không
	}
}
