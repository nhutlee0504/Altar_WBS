using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceSubject
	{
		public Task<Subjects> AddSubjectAsync(SubjectDto subject);
		public Task<Subjects> UpdateSubjectAsync(int subjectId, SubjectDto subject);
		public Task<bool> DeleteSubjectAsync(int subjectId);
		public Task<IEnumerable<Subjects>> GetAllSubjectsAsync();
		public Task<Subjects> GetSubjectByIdAsync(int subjectId);
		public Task<bool> SubjectExistsAsync(int subjectId);
	}
}
