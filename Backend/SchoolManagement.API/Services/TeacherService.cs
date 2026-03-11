using Microsoft.EntityFrameworkCore;
using SchoolManagement.API.Data;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggingService _logger;

        public TeacherService(ApplicationDbContext context, LoggingService logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _context.Teachers
                .Include(t => t.Courses)
                .ToListAsync();
        }

        public async Task<Teacher?> GetTeacherWithCourses(int id)
        {
            return await _context.Teachers
                .Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Teacher> CreateTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            _logger.Log($"Teacher created: {teacher.FirstName} {teacher.LastName}");
            return teacher;
        }

        public async Task<bool> UpdateTeacher(int id, Teacher updatedTeacher)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            teacher.FirstName = updatedTeacher.FirstName;
            teacher.LastName = updatedTeacher.LastName;

            await _context.SaveChangesAsync();
            _logger.Log($"Teacher updated: {teacher.FirstName} {teacher.LastName}");
            return true;
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            _logger.Log($"Teacher deleted with ID: {id}");
            return true;
        }
    }
}