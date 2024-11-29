namespace ALtar_WBS.Interface
{
    public interface InterfaceClassTeacher
    {
        Task AddClassTeacherAsync(int classId, int teacherId);

        Task RemoveClassTeacherAsync(int classId, int teacherId);

        Task<bool> IsTeacherAssignedToClassAsync(int classId, int teacherId);

        Task<IEnumerable<int>> GetTeachersByClassAsync(int classId);

        Task<IEnumerable<int>> GetClassesByTeacherAsync(int teacherId);
    }
}
