using SchoolManagement.API.Models;

namespace SchoolManagement.API.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student?> GetStudentById(int id);
        Task<Student> AddStudent(Student student);
        Task DeleteStudent(Student student);
    }
}