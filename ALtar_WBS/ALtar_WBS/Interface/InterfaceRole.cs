using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceRole
    {
        // Lấy tất cả các nhóm quyền
        public Task<IEnumerable<Role>> GetAllRoles();

        // Lấy thông tin nhóm quyền theo ID
        public Task<Role> GetRoleById(int roleId);

        // Tạo mới một nhóm quyền
        public Task<Role> CreateRole(string roleName);

        // Cập nhật thông tin nhóm quyền
        public Task<Role> UpdateRole(int roleId, string roleName);

        // Xóa nhóm quyền
        public Task<bool> DeleteRole(int roleId);

        // Sao chép nhóm quyền (sao chép các quyền đã tồn tại)
        public Task<Role> CopyRole(int roleId);

        // Phân quyền cho người dùng
        public Task<bool> AssignRoleToUser(int userId, int roleId);
    }
}
