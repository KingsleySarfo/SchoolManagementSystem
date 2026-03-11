using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.DTOs;
using SchoolManagement.API.Models;
using SchoolManagement.API.Services;
using BCrypt.Net;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("create-teacher")]
        public async Task<IActionResult> CreateTeacher(User user)
        {
            var createdUser = await _adminService.CreateTeacher(user);
            return Ok(new ApiResponse<User>(createdUser, "Teacher created successfully"));
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(User user)
        {
            var createdUser = await _adminService.CreateAdmin(user);
            return Ok(new ApiResponse<User>(createdUser, "Admin created successfully"));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminService.GetAllUsers();
            return Ok(new ApiResponse<IEnumerable<User>>(users));
        }
    }
}