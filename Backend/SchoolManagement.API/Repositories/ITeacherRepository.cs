using SchoolManagement.API.Models;

namespace SchoolManagement.API.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllTeachers();
        Task<Teacher?> GetTeacherById(int id);
        Task<Teacher?> GetTeacherWithCourses(int id);
        Task<Teacher> AddTeacher(Teacher teacher);
        Task<bool> UpdateTeacher(Teacher teacher);
        Task<bool> DeleteTeacher(int id);
    }
}