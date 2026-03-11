using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs;
using SchoolManagement.API.Models;
using SchoolManagement.API.Services;
using AutoMapper;

namespace SchoolManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly LoggingService _logger;

        public CourseController(ICourseService courseService, IMapper mapper, LoggingService logger)
        {
            _courseService = courseService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(new ApiResponse<IEnumerable<Course>>(courses, "Courses retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null) return NotFound(new ApiResponse<Course>("Course not found", false));

            return Ok(new ApiResponse<Course>(course, "Course retrieved successfully"));
        }

        [HttpGet("{id}/with-students")]
        public async Task<IActionResult> GetCourseWithStudents(int id)
        {
            var course = await _courseService.GetCourseWithStudents(id);
            if (course == null) return NotFound(new ApiResponse<Course>("Course not found", false));

            var result = _mapper.Map<CourseWithStudentsDto>(course);
            return Ok(new ApiResponse<CourseWithStudentsDto>(result, "Course with students retrieved successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var created = await _courseService.CreateCourse(course);
            _logger.Log($"Course created: {course.Name}");

            return Ok(new ApiResponse<Course>(created, "Course created successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            var success = await _courseService.UpdateCourse(id, course);
            if (!success) return NotFound(new ApiResponse<Course>("Course not found", false));

            _logger.Log($"Course updated: {course.Name}");
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteCourse(id);
            if (!success) return NotFound(new ApiResponse<Course>("Course not found", false));

            _logger.Log($"Course deleted with ID: {id}");
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{courseId}/assign-teacher/{teacherId}")]
        public async Task<IActionResult> AssignTeacher(int courseId, int teacherId)
        {
            var success = await _courseService.AssignTeacher(courseId, teacherId);
            if (!success) return NotFound(new ApiResponse<string>("Course or teacher not found", false));

            _logger.Log($"Teacher {teacherId} assigned to course {courseId}");
            return Ok(new ApiResponse<string>(null, "Teacher assigned successfully"));
        }
    }
}