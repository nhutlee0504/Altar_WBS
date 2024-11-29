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

        public async Task<string> LoginAdmin(LoginDto loginDto)
        {
            var user = await _context.users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName && u.IsActive == true);

            // Kiểm tra nếu người dùng không tồn tại hoặc không phải là Center Management
            if (user == null || user.Role.RoleName != "Center Management")
            {
                throw new InvalidOperationException("Invalid username or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new InvalidOperationException("Invalid username or password.");
            }

            return GenerateJwtToken(user);
        }


        public async Task<string> LoginUser(LoginDto loginDto)
        {
            var user = await _context.users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName && u.IsActive == true);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }
            return GenerateJwtToken(user);
        }


        public Task<string> Logout()
        {
            throw new NotImplementedException();
        }
        private string GenerateJwtToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            var jwtSection = _configuration.GetSection("JWT");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
        new Claim("FullName", user.FullName ?? ""),
        new Claim("Email", user.Email ?? ""),
        new Claim("RoleID", user.RoleID.ToString()),
        new Claim("UserID", user.UserID.ToString())
    };

            var token = new JwtSecurityToken(
                issuer: jwtSection["ValidIssuer"],
                audience: jwtSection["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
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
