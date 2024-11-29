using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALtar_WBS.Service
{
	public class ServiceClassTeacher : InterfaceClassTeacher
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceClassTeacher(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task AddClassTeacherAsync(int classId, int teacherId)
		{
			var classTeacher = new ClassTeachers
			{
				ClassID = classId,
				TeacherID = teacherId
			};

			try
			{
				_context.classTeachers.Add(classTeacher);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// Log exception if needed
				throw new InvalidOperationException("An error occurred while adding the class teacher.", ex);
			}
		}

		public async Task<IEnumerable<int>> GetClassesByTeacherAsync(int teacherId)
		{
			try
			{
				var classIds = await _context.classTeachers
					.Where(ct => ct.TeacherID == teacherId)
					.Select(ct => ct.ClassID)
					.ToListAsync();

				if (classIds == null || !classIds.Any())
				{
					throw new InvalidOperationException("No classes found for the specified teacher.");
				}

				return classIds;
			}
			catch (Exception ex)
			{
				// Log exception if needed
				throw new InvalidOperationException("An error occurred while retrieving classes for the teacher.", ex);
			}
		}

		public async Task<IEnumerable<int>> GetTeachersByClassAsync(int classId)
		{
			try
			{
				var teacherIds = await _context.classTeachers
					.Where(ct => ct.ClassID == classId)
					.Select(ct => ct.TeacherID)
					.ToListAsync();

				if (teacherIds == null || !teacherIds.Any())
				{
					throw new InvalidOperationException("No teachers found for the specified class.");
				}

				return teacherIds;
			}
			catch (Exception ex)
			{
				// Log exception if needed
				throw new InvalidOperationException("An error occurred while retrieving teachers for the class.", ex);
			}
		}

		public async Task<bool> IsTeacherAssignedToClassAsync(int classId, int teacherId)
		{
			try
			{
				var isAssigned = await _context.classTeachers
					.AnyAsync(ct => ct.ClassID == classId && ct.TeacherID == teacherId);

				return isAssigned;
			}
			catch (Exception ex)
			{
				// Log exception if needed
				throw new InvalidOperationException("An error occurred while checking if the teacher is assigned to the class.", ex);
			}
		}

		public async Task RemoveClassTeacherAsync(int classId, int teacherId)
		{
			try
			{
				var classTeacher = await _context.classTeachers
					.FirstOrDefaultAsync(ct => ct.ClassID == classId && ct.TeacherID == teacherId);

				if (classTeacher == null)
				{
					throw new InvalidOperationException("The specified class teacher relationship was not found.");
				}

				_context.classTeachers.Remove(classTeacher);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// Log exception if needed
				throw new InvalidOperationException("An error occurred while removing the class teacher.", ex);
			}
		}
	}
}
