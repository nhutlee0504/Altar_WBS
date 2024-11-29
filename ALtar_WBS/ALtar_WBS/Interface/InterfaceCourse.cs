using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceCourse
	{
		Task<Course> AddCourseAsync(CourseDto course);
		Task<Course> UpdateCourseAsync(int courseId, CourseDto course);
		Task<bool> DeleteCourseAsync(int courseId);
		Task<IEnumerable<Course>> GetAllCoursesAsync();
		Task<Course> GetCourseByIdAsync(int courseId);
		Task<IEnumerable<Course>> GetCoursesByFeeRangeAsync(decimal minFee, decimal maxFee);
		Task<IEnumerable<Course>> GetCoursesByDurationAsync(int duration);
		Task<IEnumerable<Course>> GetOngoingCoursesAsync();
		Task<bool> CourseExistsAsync(int courseId);
	}
}
