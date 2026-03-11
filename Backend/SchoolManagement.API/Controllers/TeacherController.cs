using SchoolManagement.API.DTOs;   // <-- THIS IS REQUIRED for ApiResponse<T>
using SchoolManagement.API.Models;
using SchoolManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly LoggingService _logger;

        public TeacherController(ITeacherService teacherService, LoggingService logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherService.GetAllTeachers();
            return Ok(new ApiResponse<IEnumerable<Teacher>>(teachers, "Teachers retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherWithCourses(int id)
        {
            var teacher = await _teacherService.GetTeacherWithCourses(id);
            if (teacher == null) return NotFound(new ApiResponse<Teacher>("Teacher not found", false));

            return Ok(new ApiResponse<Teacher>(teacher, "Teacher with courses retrieved successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(Teacher teacher)
        {
            var created = await _teacherService.CreateTeacher(teacher);
            _logger.Log($"Teacher created: {teacher.FirstName} {teacher.LastName}");

            return Ok(new ApiResponse<Teacher>(created, "Teacher created successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, Teacher teacher)
        {
            var success = await _teacherService.UpdateTeacher(id, teacher);
            if (!success) return NotFound(new ApiResponse<Teacher>("Teacher not found", false));

            _logger.Log($"Teacher updated: {teacher.FirstName} {teacher.LastName}");
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var success = await _teacherService.DeleteTeacher(id);
            if (!success) return NotFound(new ApiResponse<Teacher>("Teacher not found", false));

            _logger.Log($"Teacher deleted with ID: {id}");
            return NoContent();
        }
    }
}