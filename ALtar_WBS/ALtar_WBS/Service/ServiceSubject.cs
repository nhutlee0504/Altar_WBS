using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
    public class ServiceSubject : InterfaceSubject
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceSubject(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Subjects> AddSubjectAsync(SubjectDto subject)
        {
            try
            {
                if (subject == null)
                    throw new InvalidOperationException("Subject cannot be null.");

                var newSubject = new Subjects
                {
                    SubjectName = subject.SubjectName,
                    Level = subject.Level,
                    CategoryID = subject.CategoryID,
                };

                await _context.subjects.AddAsync(newSubject);
                await _context.SaveChangesAsync();
                return newSubject;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding subject: " + ex.Message);
            }
        }

        public async Task<Subjects> UpdateSubjectAsync(int subjectId, SubjectDto subject)
        {
            try
            {
                var existingSubject = await _context.subjects.FindAsync(subjectId);
                if (existingSubject == null)
                    throw new InvalidOperationException($"Subject with ID {subjectId} not found.");

                existingSubject.SubjectName = subject.SubjectName;
                existingSubject.Level = subject.Level;
                existingSubject.CategoryID = subject.CategoryID;

                _context.subjects.Update(existingSubject);
                await _context.SaveChangesAsync();
                return existingSubject;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating subject: " + ex.Message);
            }
        }

        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            try
            {
                var subject = await _context.subjects.FindAsync(subjectId);
                if (subject == null)
                    throw new InvalidOperationException($"Subject with ID {subjectId} not found.");

                _context.subjects.Remove(subject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error deleting subject: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Subjects>> GetAllSubjectsAsync()
        {
            try
            {
                var subjects = await _context.subjects.ToListAsync();
                if (subjects == null || !subjects.Any())
                    throw new InvalidOperationException("No subjects found.");

                return subjects;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving subjects: " + ex.Message);
            }
        }

        public async Task<Subjects> GetSubjectByIdAsync(int subjectId)
        {
            try
            {
                var subject = await _context.subjects.FirstOrDefaultAsync(s => s.SubjectID == subjectId);
                if (subject == null)
                    throw new InvalidOperationException($"Subject with ID {subjectId} not found.");

                return subject;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving subject by ID: " + ex.Message);
            }
        }

        public async Task<bool> SubjectExistsAsync(int subjectId)
        {
            try
            {
                return await _context.subjects.AnyAsync(s => s.SubjectID == subjectId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error checking if subject exists: " + ex.Message);
            }
        }
    }
}
