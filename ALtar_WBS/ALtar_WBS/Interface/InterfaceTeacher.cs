using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceTeacher
    {
        public Task<Teacher> AddTeacher(TeacherDto teacherDto, IFormFile profileImage);

        public Task<Teacher> UpdateTeacher(int teacherId, TeacherDto teacherDto, IFormFile profileImage);

        public Task<IEnumerable<Teacher>> GetAllTeachers();

        public Task<Teacher> GetTeacherById(int teacherId);

        public Task<bool> DeleteTeacher(int teacherId);

        public Task<bool> TeacherExists(int teacherId);
    }
}
