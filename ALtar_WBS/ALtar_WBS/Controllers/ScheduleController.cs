using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly InterfaceSchedule _scheduleService;

        public ScheduleController(InterfaceSchedule scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
        {
            try
            {
                var schedules = await _scheduleService.GetAllSchedulesAsync();
                return Ok(schedules);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetScheduleById(int id)
        {
            try
            {
                var schedule = await _scheduleService.GetScheduleByIdAsync(id);
                return Ok(schedule);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> CreateSchedule([FromBody] ScheduleDto scheduleDto)
        {
            try
            {
                var schedule = await _scheduleService.CreateScheduleAsync(scheduleDto);
                return CreatedAtAction(nameof(GetScheduleById), new { id = schedule.ScheduleID }, schedule);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Schedule>> UpdateSchedule(int id, [FromBody] ScheduleDto scheduleDto)
        {
            try
            {
                var schedule = await _scheduleService.UpdateScheduleAsync(id, scheduleDto);
                return Ok(schedule);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSchedule(int id)
        {
            try
            {
                var success = await _scheduleService.DeleteScheduleAsync(id);
                if (success)
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Schedule not found" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
