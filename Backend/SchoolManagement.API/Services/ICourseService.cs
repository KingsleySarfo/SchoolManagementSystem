using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course?> GetCourseById(int id);
        Task<Course?> GetCourseWithStudents(int id);
        Task<Course> CreateCourse(Course course);
        Task<bool> UpdateCourse(int id, Course course);
        Task<bool> DeleteCourse(int id);
        Task<bool> AssignTeacher(int courseId, int teacherId);
    }
}