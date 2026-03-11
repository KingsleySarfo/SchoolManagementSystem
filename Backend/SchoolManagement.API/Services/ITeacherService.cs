using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachers();
        Task<Teacher?> GetTeacherWithCourses(int id);
        Task<Teacher> CreateTeacher(Teacher teacher);
        Task<bool> UpdateTeacher(int id, Teacher teacher);
        Task<bool> DeleteTeacher(int id);
    }
}