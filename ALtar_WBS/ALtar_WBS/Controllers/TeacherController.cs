using ALtar_WBS.Dto;
using ALtar_WBS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ALtar_WBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly InterfaceTeacher _serviceTeacher;

        public TeacherController(InterfaceTeacher serviceTeacher)
        {
            _serviceTeacher = serviceTeacher;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher([FromForm] TeacherDto teacherDto, IFormFile profileImage)
        {
            try
            {
                var teacher = await _serviceTeacher.AddTeacher(teacherDto, profileImage);
                return Ok(teacher);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromForm] TeacherDto teacherDto, IFormFile profileImage)
        {
            try
            {
                var updatedTeacher = await _serviceTeacher.UpdateTeacher(id, teacherDto, profileImage);
                return Ok(updatedTeacher);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var result = await _serviceTeacher.DeleteTeacher(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _serviceTeacher.GetAllTeachers();
                return Ok(teachers);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _serviceTeacher.GetTeacherById(id);
                return Ok(teacher);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<IActionResult> TeacherExists(int id)
        {
            try
            {
                var exists = await _serviceTeacher.TeacherExists(id);
                return Ok(exists);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
