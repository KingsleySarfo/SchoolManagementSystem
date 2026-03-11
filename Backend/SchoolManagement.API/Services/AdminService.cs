using SchoolManagement.API.Data;
using SchoolManagement.API.Models;

namespace SchoolManagement.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggingService _logger;

        public AdminService(ApplicationDbContext context, LoggingService logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> CreateTeacher(User user)
        {
            user.Role = "Teacher";
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.Log($"Teacher created: {user.Username}");
            return user;
        }

        public async Task<User> CreateAdmin(User user)
        {
            user.Role = "Admin";
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.Log($"Admin created: {user.Username}");
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await Task.FromResult(_context.Users.ToList());
        }
    }
}