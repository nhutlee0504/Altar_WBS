using ALtar_WBS.Dto;
using ALtar_WBS.Model;

namespace ALtar_WBS.Interface
{
    public interface InterfaceSchedule
    {
        // Lấy danh sách tất cả các lịch
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();

        // Lấy lịch theo ID
        Task<Schedule> GetScheduleByIdAsync(int scheduleId);

        // Tạo mới một lịch
        Task<Schedule> CreateScheduleAsync(ScheduleDto scheduleDto);

        // Cập nhật lịch
        Task<Schedule> UpdateScheduleAsync(int scheduleId, ScheduleDto scheduleDto);

        // Xóa lịch
        Task<bool> DeleteScheduleAsync(int scheduleId);
    }
}
