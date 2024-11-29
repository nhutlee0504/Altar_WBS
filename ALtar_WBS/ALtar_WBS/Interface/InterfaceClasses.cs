using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceClasses
    {
        Task<IEnumerable<Classes>> GetAllClassesAsync();

        Task<IEnumerable<Classes>> GetClassesByCourseIdAsync(int courseId);

        Task<Classes> GetClassByIdAsync(int classId);

        Task<Classes> CreateClassAsync(int idCourse);

        Task<Classes> UpdateClassAsync(int classId, int idCourse);

        Task<bool> DeleteClassAsync(int classId);
    }
}
