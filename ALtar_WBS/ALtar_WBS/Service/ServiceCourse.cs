using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceCourse : InterfaceCourse
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceCourse(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Course> AddCourseAsync(CourseDto course)
		{
			try
			{
				if (course == null)
					throw new InvalidOperationException("Invalid course data");

				var newCou = new Course
				{
					CourseName = course.CourseName,
					StartDate = course.StartDate,
					EndDate = course.EndDate,
					Fee = course.Fee,
					Duration = course.Duration,
				};

				await _context.courses.AddAsync(newCou);
				await _context.SaveChangesAsync();
				return newCou;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error adding course: " + ex.Message);
			}
		}

		public async Task<bool> CourseExistsAsync(int courseId)
		{
			try
			{
				return await _context.courses.AnyAsync(c => c.CourseID == courseId);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error checking if course exists: " + ex.Message);
			}
		}

		public async Task<bool> DeleteCourseAsync(int courseId)
		{
			try
			{
				var course = await _context.courses.FindAsync(courseId);
				if (course == null)
					throw new InvalidOperationException($"Course with ID {courseId} not found");

				_context.courses.Remove(course);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error deleting course: " + ex.Message);
			}
		}

		public async Task<IEnumerable<Course>> GetAllCoursesAsync()
		{
			try
			{
				var courses = await _context.courses.ToListAsync();
				if (courses == null || !courses.Any())
					throw new InvalidOperationException("No courses found");

				return courses;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error fetching courses: " + ex.Message);
			}
		}

		public async Task<Course> GetCourseByIdAsync(int courseId)
		{
			try
			{
				var course = await _context.courses.FirstOrDefaultAsync(c => c.CourseID == courseId);
				if (course == null)
					throw new InvalidOperationException($"Course with ID {courseId} not found");

				return course;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error fetching course by ID: " + ex.Message);
			}
		}

		public async Task<IEnumerable<Course>> GetCoursesByDurationAsync(int duration)
		{
			try
			{
				var courses = await _context.courses
					.Where(c => c.Duration == duration)
					.ToListAsync();

				if (courses == null || !courses.Any())
					throw new InvalidOperationException($"No courses found for duration {duration}.");

				return courses;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error fetching courses by duration: " + ex.Message);
			}
		}

		public async Task<IEnumerable<Course>> GetCoursesByFeeRangeAsync(decimal minFee, decimal maxFee)
		{
			try
			{
				var courses = await _context.courses
					.Where(c => c.Fee >= minFee && c.Fee <= maxFee)
					.ToListAsync();

				if (courses == null || !courses.Any())
					throw new InvalidOperationException($"No courses found in the fee range {minFee} to {maxFee}.");

				return courses;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error fetching courses by fee range: " + ex.Message);
			}
		}

		public async Task<IEnumerable<Course>> GetOngoingCoursesAsync()
		{
			try
			{
				var currentDate = DateTime.UtcNow;
				var courses = await _context.courses
					.Where(c => c.StartDate <= currentDate && c.EndDate >= currentDate)
					.ToListAsync();

				if (courses == null || !courses.Any())
					throw new InvalidOperationException("No ongoing courses found");

				return courses;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error fetching ongoing courses: " + ex.Message);
			}
		}

		public async Task<Course> UpdateCourseAsync(int courseId, CourseDto course)
		{
			try
			{
				var existingCourse = await _context.courses.FindAsync(courseId);
				if (existingCourse == null)
					throw new InvalidOperationException($"Course with ID {courseId} not found");

				existingCourse.CourseName = course.CourseName;
				existingCourse.StartDate = course.StartDate;
				existingCourse.EndDate = course.EndDate;
				existingCourse.Fee = course.Fee;
				existingCourse.Duration = course.Duration;

				_context.courses.Update(existingCourse);
				await _context.SaveChangesAsync();
				return existingCourse;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error updating course: " + ex.Message);
			}
		}
	}
}
