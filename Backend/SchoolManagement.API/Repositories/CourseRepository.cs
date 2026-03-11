using Microsoft.EntityFrameworkCore;
using SchoolManagement.API.Data;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<Course?> GetCourseWithStudents(int id)
        {
            return await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> AddCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourse(Course course)
        {
            var existing = await _context.Courses.FindAsync(course.Id);
            if (existing == null) return false;

            existing.Name = course.Name;
            existing.TeacherId = course.TeacherId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignTeacher(int courseId, int teacherId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;

            course.TeacherId = teacherId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}