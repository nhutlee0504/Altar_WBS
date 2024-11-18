using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceTeacher
    {
        // Thêm giảng viên mới
        public Task<Teacher> AddTeacher(TeacherDto teacherDto, IFormFile profileImage);

        // Cập nhật thông tin giảng viên
        public Task<Teacher> UpdateTeacher(int teacherId, TeacherDto teacherDto, IFormFile profileImage);

        // Lấy tất cả giảng viên
        public Task<IEnumerable<Teacher>> GetAllTeachers();

        // Lấy giảng viên theo ID
        public Task<Teacher> GetTeacherById(int teacherId);

        // Xóa giảng viên theo ID
        public Task<bool> DeleteTeacher(int teacherId);

        // Kiểm tra xem giảng viên có tồn tại hay không
        public Task<bool> TeacherExists(int teacherId);
    }
}
