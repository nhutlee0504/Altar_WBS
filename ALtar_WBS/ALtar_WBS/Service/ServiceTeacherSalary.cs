using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ALtar_WBS.Service
{
	public class ServiceTeacherSalary : InterfaceTeacherSalary
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceTeacherSalary(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<TeacherSalary> AddSalary(TeacherSalaryDto salaryDto)
		{
			try
			{
				var user = GetUserInfoFromToken();
				var userRole = await _context.roles.FindAsync(user.RoleID);
				if (userRole.RoleName != "Accounting")
				{
					throw new InvalidOperationException("User does not have the required 'Accounting' role");
				}
				var newSalary = new TeacherSalary
				{
					TeacherID = salaryDto.TeacherID,
					amount = salaryDto.amount,
					PaymentDate = salaryDto.PaymentDate,
					PaymentType = salaryDto.PaymentType,
					Status = salaryDto.Status,
				};

				await _context.teacherSalaries.AddAsync(newSalary);
				await _context.SaveChangesAsync();

				return newSalary;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error adding salary: " + ex.Message);
			}
		}

		public async Task<TeacherSalary> UpdateSalary(int salaryId, TeacherSalaryDto salaryDto)
		{
			try
			{
				var user = GetUserInfoFromToken();
				var userRole = await _context.roles.FindAsync(user.RoleID);
				if (userRole.RoleName != "Accounting")
				{
					throw new InvalidOperationException("User does not have the required 'Accounting' role");
				}
				var existingSalary = await _context.teacherSalaries.FindAsync(salaryId);

				if (existingSalary == null)
					throw new InvalidOperationException("Salary not found.");

				existingSalary.TeacherID = salaryDto.TeacherID;
				existingSalary.amount = salaryDto.amount;
				existingSalary.PaymentDate = salaryDto.PaymentDate;
				existingSalary.PaymentType = salaryDto.PaymentType;
				existingSalary.Status = salaryDto.Status;

				_context.teacherSalaries.Update(existingSalary);
				await _context.SaveChangesAsync();

				return existingSalary;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error updating salary: " + ex.Message);
			}
		}

		public async Task<bool> DeleteSalary(int salaryId)
		{
			try
			{
				var user = GetUserInfoFromToken();
				var userRole = await _context.roles.FindAsync(user.RoleID);
				if (userRole.RoleName != "Accounting")
				{
					throw new InvalidOperationException("User does not have the required 'Accounting' role");
				}
				var salary = await _context.teacherSalaries.FindAsync(salaryId);

				if (salary == null)
					throw new InvalidOperationException("Salary not found.");

				_context.teacherSalaries.Remove(salary);
				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error deleting salary: " + ex.Message);
			}
		}

		public async Task<IEnumerable<TeacherSalary>> GetAllSalaries()
		{
			try
			{
				var salaries = await _context.teacherSalaries.ToListAsync();

				if (salaries == null || !salaries.Any())
					throw new InvalidOperationException("No salaries found.");

				return salaries;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error retrieving salaries: " + ex.Message);
			}
		}

		public async Task<TeacherSalary> GetSalaryById(int salaryId)
		{
			try
			{
				var salary = await _context.teacherSalaries.FindAsync(salaryId);

				if (salary == null)
					throw new InvalidOperationException("Salary not found.");

				return salary;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error retrieving salary by ID: " + ex.Message);
			}
		}

		public async Task<bool> SalaryExists(int salaryId)
		{
			try
			{
				return await _context.teacherSalaries.AnyAsync(s => s.SalaryID == salaryId);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error checking if salary exists: " + ex.Message);
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
