using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
	public interface InterfaceEnrollment
	{
		Task<Enrollment> AddEnrollmentAsync(EnrollmentDto enrollment);
		Task<Enrollment> UpdateEnrollmentAsync(int idenrollment, EnrollmentDto enrollment);
		Task<Enrollment> DeleteEnrollmentAsync(int enrollmentId);
		Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId);
		Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
	}
}
