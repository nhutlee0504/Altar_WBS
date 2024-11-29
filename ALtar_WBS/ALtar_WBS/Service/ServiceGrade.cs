using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceGrade : InterfaceGrade
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceGrade(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Grade> AddGradeAsync(int studentId, int courseId, float gradeValue, string? remarks)
		{
			try
			{
				var grade = new Grade
				{
					StudentID = studentId,
					CourseID = courseId,
					GradeValue = gradeValue,
					Remarks = remarks
				};

				_context.grades.Add(grade);
				await _context.SaveChangesAsync();
				return grade;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while adding the grade.", ex);
			}
		}

		public async Task<Grade> UpdateGradeAsync(int gradeId, float gradeValue, string? remarks)
		{
			try
			{
				var grade = await _context.grades.FindAsync(gradeId);

				if (grade == null)
				{
					throw new InvalidOperationException("Grade not found for update.");
				}

				grade.GradeValue = gradeValue;
				grade.Remarks = remarks;

				_context.grades.Update(grade);
				await _context.SaveChangesAsync();
				return grade;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while updating the grade.", ex);
			}
		}

		public async Task<IEnumerable<Grade>> GetGradesByStudentAsync(int studentId)
		{
			try
			{
				var grades = await _context.grades
					.Where(g => g.StudentID == studentId)
					.ToListAsync();

				return grades;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while retrieving the grades for the student.", ex);
			}
		}

		public async Task<IEnumerable<Grade>> GetGradesByCourseAsync(int courseId)
		{
			try
			{
				var grades = await _context.grades
					.Where(g => g.CourseID == courseId)
					.ToListAsync();

				return grades;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while retrieving the grades for the course.", ex);
			}
		}

		public async Task<Grade> GetGradeByIdAsync(int gradeId)
		{
			try
			{
				var grade = await _context.grades
					.FirstOrDefaultAsync(g => g.GradeID == gradeId);

				if (grade == null)
				{
					throw new InvalidOperationException("Grade not found for the provided ID.");
				}

				return grade;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while retrieving the grade.", ex);
			}
		}

		public async Task RemoveGradeAsync(int gradeId)
		{
			try
			{
				var grade = await _context.grades.FindAsync(gradeId);

				if (grade == null)
				{
					throw new InvalidOperationException("Grade not found for deletion.");
				}

				_context.grades.Remove(grade);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("An error occurred while deleting the grade.", ex);
			}
		}
	}
}
