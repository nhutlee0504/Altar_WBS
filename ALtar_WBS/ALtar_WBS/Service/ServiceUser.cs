using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
    public class ServiceUser : InterfaceUser
    {
        private readonly ApplicationDbContext _context;

        public ServiceUser(ApplicationDbContext context)
        {
            _context = context;
        }

        // Thêm một người dùng mới
        public async Task<User> AddUser(UserDto userDto)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = hashPassword,
                RoleID = userDto.RoleId,
                Phone = userDto.Phone,
                Address = userDto.Address,
                Status = "Active"
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Thêm nhiều người dùng cùng lúc
        public async Task<IEnumerable<User>> AddUsers(List<UserDto> userDtos)
        {
            var users = userDtos.Select(dto => new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleID = dto.RoleId,
                Phone = dto.Phone,
                Address = dto.Address,
                Status = "Active"
            }).ToList();

            _context.users.AddRange(users);
            await _context.SaveChangesAsync();
            return users;
        }

        // Lấy danh sách tất cả người dùng
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.users.ToListAsync();
        }

        // Lấy thông tin người dùng theo ID
        public async Task<User> GetUserById(int userId)
        {
            return await _context.users.FindAsync(userId);
        }

        // Khóa tài khoản người dùng
        public async Task<bool> LockUserAccount(int userId)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null) return false;

            user.Status = "Locked";
            await _context.SaveChangesAsync();
            return true;
        }

        // Mở khóa tài khoản người dùng
        public async Task<bool> UnlockUserAccount(int userId)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null) return false;

            user.Status = "Active";
            await _context.SaveChangesAsync();
            return true;
        }

        // Cài đặt lại mật khẩu người dùng
        public async Task<bool> ResetPassword(int userId, string newPassword)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null) return false;

            user.Password = newPassword;
            await _context.SaveChangesAsync();
            return true;
        }

        // Tìm kiếm người dùng theo từ khóa
        public async Task<IEnumerable<User>> SearchUsers(string keyword)
        {
            return await _context.users
                .Where(u => u.UserName.Contains(keyword) || u.Email.Contains(keyword))
                .ToListAsync();
        }

        // Cập nhật thông tin người dùng
        public async Task<User> UpdateUser(int userId, UserDto userDto)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null) return null;

            user.Email = userDto.Email;
            user.Address = userDto.Address;
            user.Phone = userDto.Phone;
            user.FullName = userDto.FullName;

            await _context.SaveChangesAsync();
            return user;
        }

        // Kiểm tra xem người dùng có tồn tại không
        public async Task<bool> UserExists(int userId)
        {
            return await _context.users.AnyAsync(u => u.UserID == userId);
        }
    }
}
