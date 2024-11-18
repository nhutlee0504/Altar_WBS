using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceAuth
    {
        public Task<User> CreateAdmin(UserDto userDto);
        public Task<string> LoginAdmin(LoginDto loginDto);
        public Task<string> LoginUser(LoginDto loginDto);
        public Task<string> Logout();
        public User GetUserInfoFromToken();
    }
}
