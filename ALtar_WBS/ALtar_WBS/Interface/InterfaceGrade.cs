using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceGrade
	{
		Task<Grade> AddGradeAsync(int studentId, int courseId, float gradeValue, string? remarks);
		Task<Grade> UpdateGradeAsync(int gradeId, float gradeValue, string? remarks);
		Task<IEnumerable<Grade>> GetGradesByStudentAsync(int studentId);
		Task<IEnumerable<Grade>> GetGradesByCourseAsync(int courseId);
		Task<Grade> GetGradeByIdAsync(int gradeId);
		Task RemoveGradeAsync(int gradeId);
	}
}
