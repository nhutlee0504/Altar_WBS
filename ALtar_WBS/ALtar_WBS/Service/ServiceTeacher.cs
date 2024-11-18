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
        private readonly string _imageDirectory = "wwwroot/images";  // Thư mục lưu ảnh

        public ServiceTeacher(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Teacher> AddTeacher(TeacherDto teacherDto, IFormFile profileImage)
        {
            var user = GetUserInfoFromToken();  // Lấy thông tin người dùng từ token

            if (user.UserID <= 0)
            {
                throw new ArgumentException("User ID is invalid or not provided.");
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
            };

            _context.teachers.Add(newTeacher);
            await _context.SaveChangesAsync();
            return newTeacher;
        }

        private async Task<string> SaveProfileImage(IFormFile profileImage)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _imageDirectory, profileImage.FileName);

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            // Lưu ảnh vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(stream);
            }

            return filePath; // Trả về đường dẫn tệp đã lưu
        }

        // Xóa giảng viên theo ID
        public async Task<bool> DeleteTeacher(int teacherId)
        {
            var teacher = await _context.teachers.FindAsync(teacherId);
            if (teacher == null) return false;

            _context.teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        // Lấy danh sách tất cả giảng viên
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _context.teachers.ToListAsync();
        }

        // Lấy thông tin giảng viên theo ID
        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            return await _context.teachers.FindAsync(teacherId);
        }

        // Kiểm tra xem giảng viên có tồn tại hay không
        public async Task<bool> TeacherExists(int teacherId)
        {
            return await _context.teachers.AnyAsync(t => t.UserID == teacherId);
        }

        // Cập nhật thông tin giảng viên
        public async Task<Teacher> UpdateTeacher(int teacherId, TeacherDto teacherDto, IFormFile profileImage)
        {
            var existingTeacher = await _context.teachers.FindAsync(teacherId);
            if (existingTeacher == null) return null;

            // Cập nhật ảnh nếu có
            if (profileImage != null)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(existingTeacher.ProfileImage))
                {
                    DeleteImage(existingTeacher.ProfileImage);
                }

                // Lưu ảnh mới
                existingTeacher.ProfileImage = await SaveImage(profileImage);
            }

            // Cập nhật các thông tin khác
            existingTeacher.DateOfBirth = teacherDto.DateOfBirth;
            existingTeacher.IdCard = teacherDto.IdCard;
            existingTeacher.Address = teacherDto.Address;
            existingTeacher.Phone = teacherDto.Phone;

            await _context.SaveChangesAsync();
            return existingTeacher;
        }

        // Phương thức lưu ảnh vào thư mục
        private async Task<string> SaveImage(IFormFile imageFile)
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

            return filePath;  // Trả về đường dẫn ảnh đã lưu
        }

        // Phương thức xóa ảnh
        private void DeleteImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);  // Xóa ảnh nếu tồn tại
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
