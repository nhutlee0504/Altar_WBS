using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;

namespace ALtar_WBS.Service
{
	public class ServiceEnrollment : InterfaceEnrollment
	{
		private readonly ApplicationDbContext _context;

		public ServiceEnrollment(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Enrollment> AddEnrollmentAsync(EnrollmentDto enrollment)
		{
			var n = new Enrollment
			{
				ClassID = enrollment.ClassID,
				StudentID = enrollment.StudentID,
				ErollmentDate = enrollment.ErollmentDate,
				PaymentStatus = enrollment.PaymentStatus,
				status = enrollment.status
			};
				
			_context.enrollments.Add(n);
			await _context.SaveChangesAsync();
			return n;
		}

		public async Task<Enrollment> DeleteEnrollmentAsync(int enrollmentId)
		{
			var enrollment = await _context.enrollments.FindAsync(enrollmentId);
			if (enrollment == null)
			{
				throw new NotImplementedException("Not found Enrollment");
			}

			_context.enrollments.Remove(enrollment);
			await _context.SaveChangesAsync();
			return enrollment;
		}

		public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
		{
			return await _context.enrollments.Include(e => e.Student).Include(e => e.Classes).ToListAsync();
		}

		public async Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId)
		{
			return await _context.enrollments.Include(e => e.Student).Include(e => e.Classes)
											 .FirstOrDefaultAsync(e => e.EnrollmentID == enrollmentId);
		}

		public async Task<Enrollment> UpdateEnrollmentAsync(int idEnrollment, EnrollmentDto enrollment)
		{
			var eFind = await _context.enrollments.Where(e => e.EnrollmentID == idEnrollment).FirstOrDefaultAsync();
			eFind.status = enrollment.status;
			eFind.ErollmentDate = enrollment.ErollmentDate;
			eFind.PaymentStatus = enrollment.PaymentStatus;
			eFind.StudentID = enrollment.StudentID;
			eFind.ClassID = enrollment.ClassID;
			await _context.SaveChangesAsync();
			return eFind;
		}
	}
}
