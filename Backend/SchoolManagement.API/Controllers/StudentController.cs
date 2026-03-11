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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly LoggingService _logger;

        public StudentController(IStudentService studentService, IMapper mapper, LoggingService logger)
        {
            _studentService = studentService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();
            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(new ApiResponse<IEnumerable<StudentDto>>(result, "Students retrieved successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var created = await _studentService.CreateStudent(student);
            _logger.Log($"Student created: {student.FirstName} {student.LastName}");

            var result = _mapper.Map<StudentDto>(created);
            return Ok(new ApiResponse<StudentDto>(result, "Student created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            var success = await _studentService.UpdateStudent(id, student);
            if (!success) return NotFound(new ApiResponse<Student>("Student not found", false));

            _logger.Log($"Student updated: {student.FirstName} {student.LastName}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentService.DeleteStudent(id);
            if (!success) return NotFound(new ApiResponse<Student>("Student not found", false));

            _logger.Log($"Student deleted with ID: {id}");
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null) return NotFound(new ApiResponse<Student>("Student not found", false));

            var result = _mapper.Map<StudentDto>(student);
            return Ok(new ApiResponse<StudentDto>(result, "Student retrieved successfully"));
        }

        [HttpPost("{studentId}/assign-course/{courseId}")]
        public async Task<IActionResult> AssignCourse(int studentId, int courseId)
        {
            var success = await _studentService.AssignCourse(studentId, courseId);
            if (!success) return NotFound(new ApiResponse<string>("Student or course not found", false));

            _logger.Log($"Course {courseId} assigned to student {studentId}");
            return Ok(new ApiResponse<string>(null, "Course assigned successfully"));
        }

        [HttpDelete("{studentId}/remove-course/{courseId}")]
        public async Task<IActionResult> RemoveCourse(int studentId, int courseId)
        {
            var success = await _studentService.RemoveCourse(studentId, courseId);
            if (!success) return NotFound(new ApiResponse<string>("Student or course not found", false));

            _logger.Log($"Course {courseId} removed from student {studentId}");
            return Ok(new ApiResponse<string>(null, "Course removed successfully"));
        }
    }
}