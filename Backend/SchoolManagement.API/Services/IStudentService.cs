using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student?> GetStudentById(int id);
        Task<Student> CreateStudent(Student student);
        Task<bool> UpdateStudent(int id, Student student);
        Task<bool> DeleteStudent(int id);
        Task<bool> AssignCourse(int studentId, int courseId);
        Task<bool> RemoveCourse(int studentId, int courseId);
    }
}