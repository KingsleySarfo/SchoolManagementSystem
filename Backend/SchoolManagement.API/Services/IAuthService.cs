using SchoolManagement.API.DTOs;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDTO dto);

        Task<string> Login(LoginDTO dto);
    }
}