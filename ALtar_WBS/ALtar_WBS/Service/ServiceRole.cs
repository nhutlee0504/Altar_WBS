using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
    public class ServiceRole : InterfaceRole
    {
        private ApplicationDbContext _context;
        public ServiceRole(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.roles.ToListAsync();
        }

        // Lấy nhóm quyền theo ID
        public async Task<Role> GetRoleById(int roleId)
        {
            return await _context.roles.FirstOrDefaultAsync(r => r.RoleID == roleId);
        }

        // Tạo mới một nhóm quyền
        public async Task<Role> CreateRole(string roleName)
        {
            // Kiểm tra nếu nhóm quyền đã tồn tại
            var existingRole = await _context.roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (existingRole != null)
            {
                throw new InvalidOperationException("Role already exists");
            }
            var newRole = new Role
            {
                RoleName = roleName,
            };

            _context.roles.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }

        // Cập nhật nhóm quyền
        public async Task<Role> UpdateRole(int roleId, string roleName)
        {
            var existingRole = await _context.roles.FindAsync(roleId);
            if (existingRole == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            existingRole.RoleName = roleName;

            await _context.SaveChangesAsync();
            return existingRole;
        }

        // Xóa nhóm quyền
        public async Task<bool> DeleteRole(int roleId)
        {
            var role = await _context.roles.FindAsync(roleId);
            if (role == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            _context.roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        // Sao chép nhóm quyền (tạo nhóm quyền mới với tên khác)
        public async Task<Role> CopyRole(int roleId)
        {
            var existingRole = await _context.roles.FindAsync(roleId);
            if (existingRole == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            var newRole = new Role
            {
                RoleName = existingRole.RoleName,
            };

            _context.roles.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }

        // Kiểm tra xem nhóm quyền có tồn tại không
        public async Task<bool> RoleExists(int roleId)
        {
            return await _context.roles.AnyAsync(r => r.RoleID == roleId);
        }

        // Phân quyền cho người dùng
        public async Task<bool> AssignRoleToUser(int userId, int roleId)
        {
            var user = await _context.users.FindAsync(userId);
            var role = await _context.roles.FindAsync(roleId);

            if (user == null || role == null)
            {
                throw new InvalidOperationException("User or Role not found");
            }

            user.RoleID = roleId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
