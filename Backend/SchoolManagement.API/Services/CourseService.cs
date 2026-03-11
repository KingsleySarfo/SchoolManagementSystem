using Microsoft.EntityFrameworkCore;
using SchoolManagement.API.Data;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggingService _logger;

        public CourseService(ApplicationDbContext context, LoggingService logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.Courses
                .Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            return await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> GetCourseWithStudents(int id)
        {
            return await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            _logger.Log($"Course created: {course.Name}");
            return course;
        }

        public async Task<bool> UpdateCourse(int id, Course updatedCourse)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            course.Name = updatedCourse.Name;
            course.TeacherId = updatedCourse.TeacherId;

            await _context.SaveChangesAsync();
            _logger.Log($"Course updated: {course.Name}");
            return true;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            _logger.Log($"Course deleted with ID: {id}");
            return true;
        }

        public async Task<bool> AssignTeacher(int courseId, int teacherId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var teacher = await _context.Teachers.FindAsync(teacherId);

            if (course == null || teacher == null) return false;

            course.TeacherId = teacherId;
            await _context.SaveChangesAsync();
            _logger.Log($"Teacher {teacherId} assigned to course {courseId}");
            return true;
        }
    }
}