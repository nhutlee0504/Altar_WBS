using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IO;

namespace ALtar_WBS.Service
{
	public class ServiceTeacher : InterfaceTeacher
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _imageDirectory = "wwwroot/images";

		public ServiceTeacher(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Teacher> AddTeacher(TeacherDto teacherDto, IFormFile profileImage)
		{
			try
			{
				var user = GetUserInfoFromToken();

				var roleUser = await _context.roles.FindAsync(user.RoleID);
				if (roleUser == null || roleUser.RoleName != "Teacher")
				{
					throw new InvalidOperationException("User does not have the required 'Teacher' role.");
				}

				var newTeacher = new Teacher
				{
					DateOfBirth = teacherDto.DateOfBirth,
					IdCard = teacherDto.IdCard,
					Address = teacherDto.Address,
					Phone = teacherDto.Phone,
					ProfileImage = profileImage != null ? await SaveProfileImage(profileImage) : null,
					StartDate = DateTime.Now,
					UserID = user.UserID,
					Subject = teacherDto.Subject,
				};

				_context.teachers.Add(newTeacher);
				await _context.SaveChangesAsync();
				return newTeacher;
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException($"Invalid operation: {ex.Message}");
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while adding teacher: {ex.Message}");
			}
		}

		private async Task<string> SaveProfileImage(IFormFile profileImage)
		{
			try
			{
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), _imageDirectory, profileImage.FileName);

				if (!Directory.Exists(Path.GetDirectoryName(filePath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(filePath));
				}

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await profileImage.CopyToAsync(stream);
				}

				return filePath;
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while saving the profile image: {ex.Message}");
			}
		}

		public async Task<bool> DeleteTeacher(int teacherId)
		{
			try
			{
				var teacher = await _context.teachers.FindAsync(teacherId);
				if (teacher == null)
					throw new InvalidOperationException("Teacher not found.");

				_context.teachers.Remove(teacher);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException($"Invalid operation: {ex.Message}");
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the teacher: {ex.Message}");
			}
		}

		public async Task<IEnumerable<Teacher>> GetAllTeachers()
		{
			try
			{
				return await _context.teachers.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving teachers: {ex.Message}");
			}
		}

		public async Task<Teacher> GetTeacherById(int teacherId)
		{
			try
			{
				var teacher = await _context.teachers.FindAsync(teacherId);
				if (teacher == null)
					throw new InvalidOperationException("Teacher not found.");
				return teacher;
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException($"Invalid operation: {ex.Message}");
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the teacher: {ex.Message}");
			}
		}

		public async Task<bool> TeacherExists(int teacherId)
		{
			try
			{
				return await _context.teachers.AnyAsync(t => t.UserID == teacherId);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while checking if the teacher exists: {ex.Message}");
			}
		}

		public async Task<Teacher> UpdateTeacher(int teacherId, TeacherDto teacherDto, IFormFile profileImage)
		{
			try
			{
				var existingTeacher = await _context.teachers.FindAsync(teacherId);
				if (existingTeacher == null)
					throw new InvalidOperationException("Teacher not found.");

				if (profileImage != null)
				{
					if (!string.IsNullOrEmpty(existingTeacher.ProfileImage))
					{
						DeleteImage(existingTeacher.ProfileImage);
					}

					existingTeacher.ProfileImage = await SaveImage(profileImage);
				}

				existingTeacher.DateOfBirth = teacherDto.DateOfBirth;
				existingTeacher.IdCard = teacherDto.IdCard;
				existingTeacher.Address = teacherDto.Address;
				existingTeacher.Phone = teacherDto.Phone;
				existingTeacher.Subject = teacherDto.Subject;

				await _context.SaveChangesAsync();
				return existingTeacher;
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException($"Invalid operation: {ex.Message}");
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the teacher: {ex.Message}");
			}
		}

		private async Task<string> SaveImage(IFormFile imageFile)
		{
			try
			{
				var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), _imageDirectory);

				if (!Directory.Exists(uploadDirectory))
				{
					Directory.CreateDirectory(uploadDirectory);
				}

				var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
				var filePath = Path.Combine(uploadDirectory, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await imageFile.CopyToAsync(stream);
				}

				return filePath;
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while saving the image: {ex.Message}");
			}
		}

		private void DeleteImage(string imagePath)
		{
			try
			{
				if (File.Exists(imagePath))
				{
					File.Delete(imagePath);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the image: {ex.Message}");
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
