using ALtar_WBS.Data;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceSubject : InterfaceSubject
	{
		private readonly ApplicationDbContext _context;

		public ServiceSubject(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Subjects> AddSubjectAsync(Subjects subject)
		{
			if (subject == null) return null;

			await _context.subjects.AddAsync(subject);
			await _context.SaveChangesAsync();
			return subject;
		}

		public async Task<Subjects> UpdateSubjectAsync(int subjectId, Subjects subject)
		{
			var existingSubject = await _context.subjects.FindAsync(subjectId);
			if (existingSubject == null) return null;

			existingSubject.SubjectName = subject.SubjectName;
			existingSubject.Level = subject.Level;
			existingSubject.CategoryID = subject.CategoryID;

			_context.subjects.Update(existingSubject);
			await _context.SaveChangesAsync();
			return existingSubject;
		}

		public async Task<bool> DeleteSubjectAsync(int subjectId)
		{
			var subject = await _context.subjects.FindAsync(subjectId);
			if (subject == null) return false;

			_context.subjects.Remove(subject);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Subjects>> GetAllSubjectsAsync()
		{
			return await _context.subjects
				.Include(s => s.SubjectCategories)
				.ToListAsync();
		}

		public async Task<Subjects> GetSubjectByIdAsync(int subjectId)
		{
			return await _context.subjects
				.Include(s => s.SubjectCategories)
				.FirstOrDefaultAsync(s => s.SubjectID == subjectId);
		}

		public async Task<bool> SubjectExistsAsync(int subjectId)
		{
			return await _context.subjects.AnyAsync(s => s.SubjectID == subjectId);
		}
	}
}
