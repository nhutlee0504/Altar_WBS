using ALtar_WBS.Data;
using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALtar_WBS.Service
{
    public class AttendanceService : InterfaceAttendance
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AttendanceService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Attendance> AddAttendanceAsync(AttendaceDto attendanceDto)
        {
            try
            {
                var attendance = new Attendance
                {
                    StudentID = attendanceDto.StudentID,
                    ClassID = attendanceDto.ClassID,
                    AttendanceDate = attendanceDto.AttendanceDate,
                    Status = attendanceDto.Status,
                    Remarks = attendanceDto.Remarks
                };

                _context.attendances.Add(attendance);
                await _context.SaveChangesAsync();

                return attendance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding attendance", ex);
            }
        }

        public async Task<Attendance> UpdateAttendanceAsync(int attendanceId, AttendaceDto attendanceDto)
        {
            try
            {
                var attendance = await _context.attendances.FindAsync(attendanceId);

                if (attendance == null)
                {
                    throw new InvalidOperationException("Attendance not found");
                }

                attendance.Status = attendanceDto.Status;
                attendance.Remarks = attendanceDto.Remarks;
                attendance.AttendanceDate = attendanceDto.AttendanceDate;

                _context.attendances.Update(attendance);
                await _context.SaveChangesAsync();

                return attendance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating attendance", ex);
            }
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId)
        {
            try
            {
                var attendances = await _context.attendances
                    .Where(a => a.ClassID == classId)
                    .ToListAsync();

                return attendances;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving attendances by class", ex);
            }
        }

        public async Task<Attendance> GetAttendanceByDateAsync(int studentId, DateTime date)
        {
            try
            {
                var attendance = await _context.attendances
                    .FirstOrDefaultAsync(a => a.StudentID == studentId && a.AttendanceDate == date);

                return attendance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving attendance by date", ex);
            }
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int attendanceId)
        {
            try
            {
                var attendance = await _context.attendances.FindAsync(attendanceId);

                if (attendance == null)
                {
                    throw new InvalidOperationException("Attendance not found");
                }

                return attendance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving attendance by ID", ex);
            }
        }

        public async Task RemoveAttendanceAsync(int attendanceId)
        {
            try
            {
                var attendance = await _context.attendances.FindAsync(attendanceId);

                if (attendance == null)
                {
                    throw new InvalidOperationException("Attendance not found");
                }

                _context.attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error removing attendance", ex);
            }
        }
    }
}
