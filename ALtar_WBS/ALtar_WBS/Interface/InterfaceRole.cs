using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceRole
    {
        public Task<IEnumerable<Role>> GetAllRoles();

        public Task<Role> GetRoleById(int roleId);

        public Task<Role> CreateRole(string roleName);

        public Task<Role> UpdateRole(int roleId, string roleName);

        public Task<bool> DeleteRole(int roleId);

        public Task<Role> CopyRole(int roleId);

        public Task<bool> AssignRoleToUser(int userId, int roleId);
    }
}
