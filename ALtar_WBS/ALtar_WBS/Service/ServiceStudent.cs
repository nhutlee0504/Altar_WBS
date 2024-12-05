using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
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
        public async Task<Student> AddStudent(StudentDto studentDto, IFormFile profileImage)
        {
            try
            {
                var user = GetUserInfoFromToken(); 

                if (user.UserID <= 0)
                {
                    throw new InvalidOperationException("User ID is invalid or not provided");
                }
                var userRole = await _context.roles.FindAsync(user.RoleID);
                if(userRole.RoleName != "Student")
                {
                    throw new InvalidOperationException("User does not have the required 'Student' role");
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
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding student: " + ex.Message);
            }
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            try
            {
                var student = await _context.students.FindAsync(studentId);

                if (student == null)
                {
                    throw new InvalidOperationException("Student not found");
                }

                if (!string.IsNullOrEmpty(student.ProfileImage))
                {
                    DeleteImage(student.ProfileImage);
                }

                _context.students.Remove(student);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error deleting student: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            try
            {
                var allStudents = await _context.students.ToListAsync();
                if (allStudents == null || !allStudents.Any())
                {
                    throw new InvalidOperationException("No students found");
                }
                return allStudents;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving students: " + ex.Message);
            }
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            try
            {
                var student = await _context.students.FindAsync(studentId);

                if (student == null)
                {
                    throw new InvalidOperationException("Student not found");
                }

                return student;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving student by ID: " + ex.Message);
            }
        }

        public async Task<bool> StudentExists(int studentId)
        {
            try
            {
                return await _context.students.AnyAsync(s => s.UserID == studentId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error checking if student exists: " + ex.Message);
            }
        }

        public async Task<Student> UpdateStudent(int studentId, StudentDto studentDto, IFormFile profileImage)
        {
            try
            {
                var existingStudent = await _context.students.FindAsync(studentId);

                if (existingStudent == null)
                {
                    throw new InvalidOperationException("Student not found");
                }

                existingStudent.DateOfBirth = studentDto.DateOfBirth;
                existingStudent.Address = studentDto.Address;
                existingStudent.ParentPhone = studentDto.ParentPhone;

                if (profileImage != null)
                {
                    if (!string.IsNullOrEmpty(existingStudent.ProfileImage))
                    {
                        DeleteImage(existingStudent.ProfileImage);
                    }
                    existingStudent.ProfileImage = await SaveImage(profileImage);
                }

                await _context.SaveChangesAsync();

                return existingStudent;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating student: " + ex.Message);
            }
        }

        private async Task<string> SaveImage(IFormFile imageFile)
        {
            try
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
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error saving image: " + ex.Message);
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
                throw new InvalidOperationException("Error deleting image: " + ex.Message);
            }
        }
        public User GetUserInfoFromToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is missing or invalid");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken == null)
            {
                throw new ArgumentException("Invalid JWT token");
            }

            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var roleID = jwtToken.Claims.FirstOrDefault(c => c.Type == "RoleID")?.Value;
            var userID = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                throw new ArgumentException("Required claims (UserID or RoleID) are missing from the token");
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
