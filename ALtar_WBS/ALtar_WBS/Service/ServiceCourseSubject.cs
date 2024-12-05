using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
    public class ServiceCourseSubject : InterfaceCourseSubject
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceCourseSubject(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CourseSubject> AddCourseSubjectAsync(int courseId, int subjectId)
        {
            try
            {
                var courseSubject = new CourseSubject
                {
                    CourseID = courseId,
                    SubjectID = subjectId
                };

                _context.courseSubjects.Add(courseSubject);
                await _context.SaveChangesAsync();

                return courseSubject;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding Course-Subject: {ex.Message}");
            }
        }

        public async Task<bool> CourseSubjectExistsAsync(int courseId, int subjectId)
        {
            try
            {
                return await _context.courseSubjects
                    .AnyAsync(cs => cs.CourseID == courseId && cs.SubjectID == subjectId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error checking Course-Subject existence: {ex.Message}");
            }
        }

        public async Task<bool> DeleteCourseSubjectAsync(int csId)
        {
            try
            {
                var courseSubject = await _context.courseSubjects.FindAsync(csId);
                if (courseSubject == null)
                    throw new InvalidOperationException("Course-Subject relationship not found");

                _context.courseSubjects.Remove(courseSubject);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting Course-Subject: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesBySubjectAsync(int subjectId)
        {
            try
            {
                var courses = await _context.courseSubjects
                    .Where(cs => cs.SubjectID == subjectId)
                    .Select(cs => cs.Course)
                    .ToListAsync();

                if (courses == null || !courses.Any())
                    throw new InvalidOperationException("No courses found for the specified subject");

                return courses;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving courses by subject: {ex.Message}");
            }
        }

        public async Task<CourseSubject> GetCourseSubjectByIdAsync(int csId)
        {
            try
            {
                var courseSubject = await _context.courseSubjects
                    .Include(cs => cs.Course)
                    .Include(cs => cs.Subjects)
                    .FirstOrDefaultAsync(cs => cs.csID == csId);

                if (courseSubject == null)
                    throw new InvalidOperationException("Course-Subject relationship not found");

                return courseSubject;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving Course-Subject by ID: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Subjects>> GetSubjectsByCourseAsync(int courseId)
        {
            try
            {
                var subjects = await _context.courseSubjects
                    .Where(cs => cs.CourseID == courseId)
                    .Select(cs => cs.Subjects)
                    .ToListAsync();

                if (subjects == null || !subjects.Any())
                    throw new InvalidOperationException("No subjects found for the specified course.");

                return subjects;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving subjects by course: {ex.Message}");
            }
        }

        public async Task<CourseSubject> UpdateCourseSubjectAsync(int csId, int courseId, int subjectId)
        {
            try
            {
                var courseSubject = await _context.courseSubjects.FindAsync(csId);
                if (courseSubject == null)
                    throw new InvalidOperationException("Course-Subject relationship not found");

                courseSubject.CourseID = courseId;
                courseSubject.SubjectID = subjectId;

                _context.courseSubjects.Update(courseSubject);
                await _context.SaveChangesAsync();

                return courseSubject;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating Course-Subject: {ex.Message}");
            }
        }
    }
}
