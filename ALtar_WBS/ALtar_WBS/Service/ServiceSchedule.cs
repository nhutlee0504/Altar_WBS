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
    public class ServiceSchedule : InterfaceSchedule
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ServiceSchedule(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            try
            {
                var schedules = await _context.schedules
                    .ToListAsync();

                if (schedules == null || !schedules.Any())
                {
                    throw new InvalidOperationException("No schedules found");
                }

                return schedules;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving schedules: " + ex.Message);
            }
        }

        public async Task<Schedule> GetScheduleByIdAsync(int scheduleId)
        {
            try
            {
                var schedule = await _context.schedules
                    .FirstOrDefaultAsync(s => s.ScheduleID == scheduleId);

                if (schedule == null)
                {
                    throw new InvalidOperationException("Schedule not found");
                }

                return schedule;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the schedule: " + ex.Message);
            }
        }

        public async Task<Schedule> CreateScheduleAsync(ScheduleDto scheduleDto)
        {
            try
            {
                var schedule = new Schedule
                {
                    DayOfWeek = scheduleDto.DayOfWeek,
                    StartTime = scheduleDto.StartTime,
                    EndTime = scheduleDto.EndTime,
                    Room = scheduleDto.Room,
                    ClassID = scheduleDto.ClassID
                };

                _context.schedules.Add(schedule);
                await _context.SaveChangesAsync();

                return schedule;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the schedule: " + ex.Message);
            }
        }

        public async Task<Schedule> UpdateScheduleAsync(int scheduleId, ScheduleDto scheduleDto)
        {
            try
            {
                var schedule = await _context.schedules.FindAsync(scheduleId);

                if (schedule == null)
                {
                    throw new InvalidOperationException("Schedule not found");
                }

                schedule.DayOfWeek = scheduleDto.DayOfWeek;
                schedule.StartTime = scheduleDto.StartTime;
                schedule.EndTime = scheduleDto.EndTime;
                schedule.Room = scheduleDto.Room;
                schedule.ClassID = scheduleDto.ClassID;

                await _context.SaveChangesAsync();

                return schedule;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while updating the schedule: " + ex.Message);
            }
        }

        public async Task<bool> DeleteScheduleAsync(int scheduleId)
        {
            try
            {
                var schedule = await _context.schedules.FindAsync(scheduleId);

                if (schedule == null)
                {
                    throw new InvalidOperationException("Schedule not found");
                }

                _context.schedules.Remove(schedule);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the schedule: " + ex.Message);
            }
        }
    }
}
