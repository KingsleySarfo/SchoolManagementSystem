using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public interface IAdminService
    {
        Task<User> CreateTeacher(User user);
        Task<User> CreateAdmin(User user);
        Task<IEnumerable<User>> GetAllUsers();
    }
}