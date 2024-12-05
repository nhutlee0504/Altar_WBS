using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceSchedule
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();

        Task<Schedule> GetScheduleByIdAsync(int scheduleId);

        Task<Schedule> CreateScheduleAsync(ScheduleDto scheduleDto);

        Task<Schedule> UpdateScheduleAsync(int scheduleId, ScheduleDto scheduleDto);

        Task<bool> DeleteScheduleAsync(int scheduleId);
    }
}
