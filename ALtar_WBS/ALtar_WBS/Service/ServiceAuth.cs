using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ALtar_WBS.Service
{
    public class ServiceAuth : InterfaceAuth
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceAuth(ApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> CreateAdmin(UserDto userDto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var newadmin = new User
            {
                UserName = userDto.UserName,
                Password = hashedPassword,
                FullName = userDto.FullName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address,
                Status = "Active",
                RoleID = userDto.RoleId,
            };
            await _context.users.AddAsync(newadmin);
            await _context.SaveChangesAsync();
            return newadmin;
        }

        public async Task<string> LoginAdmin(LoginDto loginDto)
        {
            var user = await _context.users
          .Include(u => u.Role)
          .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName && u.Status == "Active");
            if (user == null || user.Role.RoleName != "Admin" || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }
            return GenerateJwtToken(user);
        }

        public async Task<string> LoginUser(LoginDto loginDto)
        {
            var user = await _context.users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName && u.Status == "Active");

            // Kiểm tra nếu người dùng không tồn tại hoặc mật khẩu không đúng
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }

            // Tạo và trả về JWT token
            return GenerateJwtToken(user);
        }


        public Task<string> Logout()
        {
            throw new NotImplementedException();
        }
        private string GenerateJwtToken(User user)
        {
            // Kiểm tra nếu user null thì trả về null hoặc thông báo lỗi
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var jwtSection = _configuration.GetSection("JWT");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cấu hình các claim, thay RoleID bằng RoleName
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim("FullName", user.FullName ?? ""), // Nếu FullName là null, sử dụng chuỗi rỗng
        new Claim("Email", user.Email ?? ""), // Nếu Email là null, sử dụng chuỗi rỗng
        new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User"), // Lấy RoleName thay vì RoleID
        new Claim("UserID", user.UserID.ToString()) // Thêm UserID vào claims
    };

            // Tạo JWT token với các claims và credentials
            var token = new JwtSecurityToken(
                issuer: jwtSection["ValidIssuer"],
                audience: jwtSection["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            // Trả về JWT token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public User GetUserInfoFromToken()
        {
            // Lấy token từ header Authorization
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token is missing or invalid.");
            }

            // Giải mã token và lấy thông tin người dùng từ claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userName = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var fullName = jwtToken?.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
            var email = jwtToken?.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var roleName = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; // Lấy RoleName
            var userID = jwtToken?.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            if (userID == null)
            {
                throw new ArgumentException("UserID is not found in the token.");
            }

            // Tạo đối tượng User từ các claim
            var user = new User
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                UserID = int.Parse(userID) // Nếu UserID là số, chuyển đổi sang int
            };

            return user;
        }

    }
}
