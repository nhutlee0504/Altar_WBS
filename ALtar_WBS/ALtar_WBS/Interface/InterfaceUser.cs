using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceUser
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(int userId);
        public Task<User> AddUser(UserDto userDto);
        public Task<User> UpdateUser(int userId, UserDto userDto);
        public Task<bool> ResetPassword(int userId, string newPassword);
        public Task<bool> LockUserAccount(int userId);
        public Task<bool> UnlockUserAccount(int userId);
        public Task<IEnumerable<User>> SearchUsers(string keyword);
    }
}
