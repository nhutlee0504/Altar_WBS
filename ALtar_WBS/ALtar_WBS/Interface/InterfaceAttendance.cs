using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceAttendance
    {
        Task<Attendance> AddAttendanceAsync(AttendaceDto attendanceDto);

        Task<Attendance> UpdateAttendanceAsync(int attendanceId, AttendaceDto attendanceDto);

        Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId);

        Task<Attendance> GetAttendanceByDateAsync(int studentId, DateTime date);

        Task<Attendance> GetAttendanceByIdAsync(int attendanceId);

        Task RemoveAttendanceAsync(int attendanceId);
    }
}
