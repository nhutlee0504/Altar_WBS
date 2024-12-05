using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceStudent
	{
		public Task<Student> AddStudent(StudentDto studentDto, IFormFile profileImage);
		public Task<bool> DeleteStudent(int studentId);
		public Task<IEnumerable<Student>> GetAllStudents();
		public Task<Student> GetStudentById(int studentId);
		public Task<bool> StudentExists(int studentId);
		public Task<Student> UpdateStudent(int studentId, StudentDto studentDto, IFormFile profileImage);

	}
}
