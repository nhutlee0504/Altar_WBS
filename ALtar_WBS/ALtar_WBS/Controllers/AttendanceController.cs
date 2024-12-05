using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using ALtar_WBS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly InterfaceAttendance _attendanceService;

        public AttendanceController(InterfaceAttendance attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAttendance([FromBody] AttendaceDto attendanceDto)
        {
            try
            {
                var attendance = await _attendanceService.AddAttendanceAsync(attendanceDto);
                return CreatedAtAction(nameof(GetAttendanceById), new { attendanceId = attendance.AttendanceID }, attendance);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while adding attendance.", details = ex.Message });
            }
        }

        [HttpPut("update/{attendanceId}")]
        public async Task<IActionResult> UpdateAttendance(int attendanceId, [FromBody] AttendaceDto attendanceDto)
        {
            try
            {
                var attendance = await _attendanceService.UpdateAttendanceAsync(attendanceId, attendanceDto);
                return Ok(attendance);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating attendance.", details = ex.Message });
            }
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetAttendancesByClass(int classId)
        {
            try
            {
                var attendances = await _attendanceService.GetAttendancesByClassAsync(classId);
                if (attendances == null || !attendances.Any())
                {
                    return NotFound(new { message = "No attendance records found for the class." });
                }
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving attendances.", details = ex.Message });
            }
        }

        [HttpGet("student/{studentId}/date/{date}")]
        public async Task<IActionResult> GetAttendanceByDate(int studentId, DateTime date)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceByDateAsync(studentId, date);
                if (attendance == null)
                {
                    return NotFound(new { message = "Attendance not found for the student on this date." });
                }
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the attendance by date.", details = ex.Message });
            }
        }

        [HttpGet("{attendanceId}")]
        public async Task<IActionResult> GetAttendanceById(int attendanceId)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceByIdAsync(attendanceId);
                return Ok(attendance);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the attendance.", details = ex.Message });
            }
        }

        [HttpDelete("{attendanceId}")]
        public async Task<IActionResult> RemoveAttendance(int attendanceId)
        {
            try
            {
                await _attendanceService.RemoveAttendanceAsync(attendanceId);
                return NoContent(); // 204 No Content
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the attendance.", details = ex.Message });
            }
        }
    }
}
