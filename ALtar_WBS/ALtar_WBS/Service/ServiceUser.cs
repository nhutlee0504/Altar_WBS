using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ALtar_WBS.Service
{
	public class ServiceUser : InterfaceUser
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceUser(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<User> AddUser(UserDto userDto)
		{
			var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			if (!Regex.IsMatch(userDto.Email, emailRegex))
			{
				throw new InvalidOperationException("Invalid email format");
			}

			var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$";
			if (!Regex.IsMatch(userDto.Password, passwordRegex))
			{
				throw new InvalidOperationException("Password must be at least 8 characters long and contain a mix of upper and lower case letters, and numbers");
			}

			var phoneRegex = @"^0\d{9}$";
			if (!Regex.IsMatch(userDto.Phone, phoneRegex))
			{
				throw new InvalidOperationException("Phone number must be 10 digits and start with 0");
			}

			var hashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
			var user = new User
			{
				UserName = userDto.UserName,
				FullName = userDto.FullName,
				Email = userDto.Email,
				Password = hashPassword,
				RoleID = (int)userDto.RoleId,
				Phone = userDto.Phone,
				Address = userDto.Address,
				IsActive = true
			};

			_context.users.Add(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			var allUsers = await _context.users.ToListAsync();
			if (allUsers.Count == null)
			{
				throw new InvalidOperationException("Users not found");
			}
			return allUsers;
		}

		public async Task<User> GetUserById(int userId)
		{
			var userFind = await _context.users.FindAsync(userId);
			if (userFind == null)
			{
				throw new InvalidOperationException("User with userID not found");
			}
			return userFind;
		}

		public async Task<bool> LockUserAccount(int userId)
		{
			var user = await _context.users.FindAsync(userId);
			if (user == null) return false;

			user.IsActive = false;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UnlockUserAccount(int userId)
		{
			var user = await _context.users.FindAsync(userId);
			if (user == null) return false;

			user.IsActive = false;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> ResetPassword(int userId, string newPassword)
		{
			var user = await _context.users.FindAsync(userId);
			if (user == null) return false;

			user.Password = newPassword;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<User>> SearchUsers(string keyword)
		{
			var userFind = await _context.users
				.Where(u => u.UserName.Contains(keyword) || u.Email.Contains(keyword))
				.ToListAsync();
			if (userFind == null)
			{
				throw new InvalidOperationException("Not found user with keyword");
			}
			return userFind;
		}

		public async Task<User> UpdateUser(int userId, UserDto userDto)
		{
			var user = await _context.users.FindAsync(userId);
			if (user == null)
			{
				throw new InvalidOperationException("User not found");
			}

			var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			if (!Regex.IsMatch(userDto.Email, emailRegex))
			{
				throw new InvalidOperationException("Invalid email format.");
			}

			var phoneRegex = @"^0\d{9}$";
			if (!Regex.IsMatch(userDto.Phone, phoneRegex))
			{
				throw new InvalidOperationException("Phone number must be 10 digits and start with 0.");
			}

			user.Email = userDto.Email;
			user.Address = userDto.Address;
			user.Phone = userDto.Phone;
			user.FullName = userDto.FullName;

			await _context.SaveChangesAsync();
			return user;
		}
	}
}
