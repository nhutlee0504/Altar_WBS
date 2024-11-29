using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceRole : InterfaceRole
	{
		private ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceRole(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IEnumerable<Role>> GetAllRoles()
		{
			var allRole = await _context.roles.ToListAsync();
			if (allRole == null)
			{
				throw new NotImplementedException("No roles found");
			}
			return allRole;
		}

		public async Task<Role> GetRoleById(int roleId)
		{
			var roleFind = await _context.roles.FirstOrDefaultAsync(r => r.RoleID == roleId);
			if (roleFind == null)
			{
				throw new NotImplementedException("Role with roleId was not found");
			}
			return roleFind;
		}

		public async Task<Role> CreateRole(string roleName)
		{
			var validRoles = new List<string>
			{
				"Center Management",
				"Enrollment Department",
				"Accounting",
				"Teacher",
				"Student",
				"Guest"
			};

			if (!validRoles.Contains(roleName))
			{
				throw new ArgumentException("Invalid role name. Allowed roles are: Center Management, Enrollment Department, Accounting, Teacher, Student, Guest.");
			}

			var existingRole = await _context.roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
			if (existingRole != null)
			{
				throw new InvalidOperationException("Role already exists.");
			}

			var newRole = new Role
			{
				RoleName = roleName,
			};

			_context.roles.Add(newRole);
			await _context.SaveChangesAsync();
			return newRole;
		}

		public async Task<Role> UpdateRole(int roleId, string roleName)
		{
			var validRoles = new List<string>
			{
				"Center Management",
				"Enrollment Department",
				"Accounting",
				"Teacher",
				"Student",
				"Guest"
			};

			if (!validRoles.Contains(roleName))
			{
				throw new ArgumentException("Invalid role name. Allowed roles are: Center Management, Enrollment Department, Accounting, Teacher, Student, Guest.");
			}

			var existingRole = await _context.roles.FindAsync(roleId);
			if (existingRole == null)
			{
				throw new InvalidOperationException("Role not found");
			}

			var duplicateRole = await _context.roles.FirstOrDefaultAsync(r => r.RoleName == roleName && r.RoleID != roleId);
			if (duplicateRole != null)
			{
				throw new InvalidOperationException("A role with the same name already exists.");
			}

			existingRole.RoleName = roleName;
			await _context.SaveChangesAsync();
			return existingRole;
		}

		public async Task<bool> DeleteRole(int roleId)
		{
			var role = await _context.roles.FindAsync(roleId);
			if (role == null)
			{
				throw new NotImplementedException("Role not found");
			}

			_context.roles.Remove(role);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<Role> CopyRole(int roleId)
		{
			var existingRole = await _context.roles.FindAsync(roleId);
			if (existingRole == null)
			{
				throw new NotImplementedException("Role not found");
			}

			var newRole = new Role
			{
				RoleName = existingRole.RoleName,
			};

			_context.roles.Add(newRole);
			await _context.SaveChangesAsync();
			return newRole;
		}

		public async Task<bool> AssignRoleToUser(int userId, int roleId)
		{
			var user = await _context.users.FindAsync(userId);
			var role = await _context.roles.FindAsync(roleId);

			if (user == null || role == null)
			{
				throw new NotImplementedException("User or Role not found");
			}

			user.RoleID = roleId;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
