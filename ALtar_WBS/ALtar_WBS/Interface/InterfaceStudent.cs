using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceStudent
	{
		public Task<Student> AddStudent(StudentDto studentDto, IFormFile profileImage); // Thêm sinh viên
		public Task<bool> DeleteStudent(int studentId); // Xóa sinh viên
		public Task<IEnumerable<Student>> GetAllStudents(); // Lấy danh sách sinh viên
		public Task<Student> GetStudentById(int studentId); // Lấy sinh viên theo ID
		public Task<bool> StudentExists(int studentId); // Kiểm tra sinh viên có tồn tại
		public Task<Student> UpdateStudent(int studentId, StudentDto studentDto, IFormFile profileImage); // Cập nhật sinh viên

	}
}
