using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface ISubjectCategory
	{
		public Task<SubjectCategories> AddCategoryAsync(string categoryName);
		public Task<SubjectCategories> UpdateCategoryAsync(int categoryId, string categoryName);
		public Task<bool> DeleteCategoryAsync(int categoryId);
		public Task<IEnumerable<SubjectCategories>> GetAllCategoriesAsync();  
		public Task<SubjectCategories> GetCategoryByIdAsync(int categoryId);  
		public Task<bool> CategoryExistsAsync(int categoryId); 
	}
}
