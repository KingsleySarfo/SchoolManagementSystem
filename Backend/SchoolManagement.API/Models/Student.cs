using System.Collections.Generic;

namespace SchoolManagement.API.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Grade { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Navigation Property (Many-to-Many)
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}