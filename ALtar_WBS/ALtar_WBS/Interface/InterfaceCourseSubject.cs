using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceCourseSubject
	{
		Task<CourseSubject> AddCourseSubjectAsync(int courseId, int subjectId);
		Task<CourseSubject> UpdateCourseSubjectAsync(int csId, int courseId, int subjectId);
		Task<bool> DeleteCourseSubjectAsync(int csId);
		Task<IEnumerable<Subjects>> GetSubjectsByCourseAsync(int courseId);
		Task<IEnumerable<Course>> GetCoursesBySubjectAsync(int subjectId);
		Task<bool> CourseSubjectExistsAsync(int courseId, int subjectId);
		Task<CourseSubject> GetCourseSubjectByIdAsync(int csId);
	}
}
