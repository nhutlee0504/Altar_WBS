using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ALtar_WBS.Service
{
	public class ServiceStudent : InterfaceStudent
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _imageDirectory = "wwwroot/images/students";

		public ServiceStudent(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		// Thêm sinh viên
		public async Task<Student> AddStudent(StudentDto studentDto, IFormFile profileImage)
		{
			var user = GetUserInfoFromToken();  // Lấy thông tin người dùng từ token

			if (user.UserID <= 0)
			{
				throw new ArgumentException("User ID is invalid or not provided.");
			}
			var profileImagePath = profileImage != null ? await SaveImage(profileImage) : null;

			var newStudent = new Student
			{
				DateOfBirth = studentDto.DateOfBirth,
				Address = studentDto.Address,
				ParentPhone = studentDto.ParentPhone,
				ProfileImage = profileImagePath,
				UserID = user.UserID,
			};

			_context.students.Add(newStudent);
			await _context.SaveChangesAsync();
			return newStudent;
		}

		// Xóa sinh viên
		public async Task<bool> DeleteStudent(int studentId)
		{
			var student = await _context.students.FindAsync(studentId);

			if (student == null) return false;

			if (!string.IsNullOrEmpty(student.ProfileImage))
			{
				DeleteImage(student.ProfileImage);
			}

			_context.students.Remove(student);
			await _context.SaveChangesAsync();
			return true;
		}

		// Lấy danh sách tất cả sinh viên
		public async Task<IEnumerable<Student>> GetAllStudents()
		{
			return await _context.students.ToListAsync();
		}

		// Lấy thông tin sinh viên theo ID
		public async Task<Student> GetStudentById(int studentId)
		{
			return await _context.students.FindAsync(studentId);
		}

		// Kiểm tra sinh viên có tồn tại không
		public async Task<bool> StudentExists(int studentId)
		{
			return await _context.students.AnyAsync(s => s.UserID == studentId);
		}

		// Cập nhật thông tin sinh viên
		public async Task<Student> UpdateStudent(int studentId, StudentDto studentDto, IFormFile profileImage)
		{
			// Tìm sinh viên theo ID
			var existingStudent = await _context.students.FindAsync(studentId);

			if (existingStudent == null)
			{
				throw new ArgumentException("Student not found.");
			}

			// Cập nhật thông tin cơ bản
			existingStudent.DateOfBirth = studentDto.DateOfBirth;
			existingStudent.Address = studentDto.Address;
			existingStudent.ParentPhone = studentDto.ParentPhone;

			// Nếu có ảnh mới, xử lý cập nhật ảnh
			if (profileImage != null)
			{
				// Xóa ảnh cũ nếu tồn tại
				if (!string.IsNullOrEmpty(existingStudent.ProfileImage))
				{
					DeleteImage(existingStudent.ProfileImage);
				}

				// Lưu ảnh mới và cập nhật đường dẫn ảnh
				existingStudent.ProfileImage = await SaveImage(profileImage);
			}

			// Lưu thay đổi vào cơ sở dữ liệu
			await _context.SaveChangesAsync();
			return existingStudent;
		}


		// Lưu ảnh vào thư mục
		private async Task<string> SaveImage(IFormFile imageFile)
		{
			if (!Directory.Exists(_imageDirectory))
			{
				Directory.CreateDirectory(_imageDirectory);
			}

			var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
			var filePath = Path.Combine(_imageDirectory, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await imageFile.CopyToAsync(stream);
			}

			return filePath;
		}

		// Xóa ảnh
		private void DeleteImage(string imagePath)
		{
			if (File.Exists(imagePath))
			{
				File.Delete(imagePath);
			}
		}

		// Lấy thông tin người dùng từ token
		public User GetUserInfoFromToken()
		{
			var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentException("Token is missing or invalid.");
			}

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			var userName = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
			var fullName = jwtToken?.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
			var email = jwtToken?.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
			var roleName = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
			var userID = jwtToken?.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

			if (userID == null)
			{
				throw new ArgumentException("UserID is not found in the token.");
			}

			return new User
			{
				UserName = userName,
				FullName = fullName,
				Email = email,
				UserID = int.Parse(userID)
			};
		}
	}
}
