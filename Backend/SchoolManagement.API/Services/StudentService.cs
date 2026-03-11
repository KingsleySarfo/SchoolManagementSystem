using Microsoft.EntityFrameworkCore;
using SchoolManagement.API.Data;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggingService _logger;

        public StudentService(ApplicationDbContext context, LoggingService logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students
                .Include(s => s.Courses)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            _logger.Log($"Student created: {student.FirstName} {student.LastName}");
            return student;
        }

        public async Task<bool> UpdateStudent(int id, Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;
            student.Email = updatedStudent.Email;

            await _context.SaveChangesAsync();
            _logger.Log($"Student updated: {student.FirstName} {student.LastName}");
            return true;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            _logger.Log($"Student deleted with ID: {id}");
            return true;
        }

        public async Task<bool> AssignCourse(int studentId, int courseId)
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return false;

            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;

            if (!student.Courses.Contains(course))
            {
                student.Courses.Add(course);
                await _context.SaveChangesAsync();
                _logger.Log($"Course {courseId} assigned to student {studentId}");
            }

            return true;
        }

        public async Task<bool> RemoveCourse(int studentId, int courseId)
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return false;

            var course = student.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null) return false;

            student.Courses.Remove(course);
            await _context.SaveChangesAsync();
            _logger.Log($"Course {courseId} removed from student {studentId}");

            return true;
        }
    }
}