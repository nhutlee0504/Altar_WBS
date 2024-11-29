using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceSubject
	{
		public Task<Subjects> AddSubjectAsync(SubjectDto subject); // Thêm môn học
		public Task<Subjects> UpdateSubjectAsync(int subjectId, SubjectDto subject); // Cập nhật môn học
		public Task<bool> DeleteSubjectAsync(int subjectId); // Xóa môn học
		public Task<IEnumerable<Subjects>> GetAllSubjectsAsync(); // Lấy danh sách môn học
		public Task<Subjects> GetSubjectByIdAsync(int subjectId); // Lấy môn học theo ID
		public Task<bool> SubjectExistsAsync(int subjectId); // Kiểm tra môn học có tồn tại không
	}
}
