using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ALtar_WBS.Service
{
	public class SubjectCategoryService : ISubjectCategory
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SubjectCategoryService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<SubjectCategories> AddCategoryAsync(string categoryName)
		{
			try
			{
				var category = new SubjectCategories
				{
					CategoryName = categoryName
				};
				_context.subjectCategories.Add(category);
				await _context.SaveChangesAsync();
				return category;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error adding category: " + ex.Message);
			}
		}

		public async Task<SubjectCategories> UpdateCategoryAsync(int categoryId, string categoryName)
		{
			try
			{
				var category = await _context.subjectCategories.FindAsync(categoryId);
				if (category == null)
					throw new InvalidOperationException("Category not found.");

				category.CategoryName = categoryName;
				_context.subjectCategories.Update(category);
				await _context.SaveChangesAsync();
				return category;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error updating category: " + ex.Message);
			}
		}

		public async Task<bool> DeleteCategoryAsync(int categoryId)
		{
			try
			{
				var category = await _context.subjectCategories.FindAsync(categoryId);
				if (category == null)
					throw new InvalidOperationException("Category not found.");

				_context.subjectCategories.Remove(category);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error deleting category: " + ex.Message);
			}
		}

		public async Task<IEnumerable<SubjectCategories>> GetAllCategoriesAsync()
		{
			try
			{
				var categories = await _context.subjectCategories.ToListAsync();
				if (categories == null || !categories.Any())
					throw new InvalidOperationException("No categories found.");

				return categories;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error retrieving categories: " + ex.Message);
			}
		}

		public async Task<SubjectCategories> GetCategoryByIdAsync(int categoryId)
		{
			try
			{
				var category = await _context.subjectCategories.FindAsync(categoryId);
				if (category == null)
					throw new InvalidOperationException("Category not found.");

				return category;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error retrieving category by ID: " + ex.Message);
			}
		}

		public async Task<bool> CategoryExistsAsync(int categoryId)
		{
			try
			{
				return await _context.subjectCategories.AnyAsync(c => c.CategoryID == categoryId);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error checking if category exists: " + ex.Message);
			}
		}

		public User GetUserInfoFromToken()
		{
			var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentException("Token is missing or invalid.");
			}

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			if (jwtToken == null)
			{
				throw new ArgumentException("Invalid JWT token.");
			}

			var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
			var fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
			var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
			var roleID = jwtToken.Claims.FirstOrDefault(c => c.Type == "RoleID")?.Value;
			var userID = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

			if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
			{
				throw new ArgumentException("Required claims (UserID or RoleID) are missing from the token.");
			}

			var user = new User
			{
				UserName = userName,
				FullName = fullName,
				Email = email,
				UserID = int.Parse(userID),
				RoleID = int.Parse(roleID)
			};

			return user;
		}
	}
}
