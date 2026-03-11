using SchoolManagement.API.Models;

namespace SchoolManagement.API.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course?> GetCourseById(int id);
        Task<Course?> GetCourseWithStudents(int id);
        Task<Course> AddCourse(Course course);
        Task<bool> UpdateCourse(Course course);
        Task<bool> DeleteCourse(int id);
        Task<bool> AssignTeacher(int courseId, int teacherId);
    }
}