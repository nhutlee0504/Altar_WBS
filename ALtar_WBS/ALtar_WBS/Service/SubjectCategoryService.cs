using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class SubjectCategoryService :ISubjectCategory
	{
		private readonly ApplicationDbContext _context;

		public SubjectCategoryService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<SubjectCategories> AddCategoryAsync(string categoryName)
		{
			var category = new SubjectCategories
			{
				CategoryName = categoryName
			};
			_context.subjectCategories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<SubjectCategories> UpdateCategoryAsync(int categoryId, string categoryName)
		{
			var category = await _context.subjectCategories.FindAsync(categoryId);
			if (category == null) return null;

			category.CategoryName = categoryName;
			_context.subjectCategories.Update(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<bool> DeleteCategoryAsync(int categoryId)
		{
			var category = await _context.subjectCategories.FindAsync(categoryId);
			if (category == null) return false;

			_context.subjectCategories.Remove(category);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<SubjectCategories>> GetAllCategoriesAsync()
		{
			return await _context.subjectCategories.ToListAsync();
		}

		public async Task<SubjectCategories> GetCategoryByIdAsync(int categoryId)
		{
			return await _context.subjectCategories.FindAsync(categoryId);
		}

		public async Task<bool> CategoryExistsAsync(int categoryId)
		{
			return await _context.subjectCategories.AnyAsync(c => c.CategoryID == categoryId);
		}
	}
}
