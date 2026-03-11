namespace SchoolManagement.API.DTOs
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Grade { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    public class StudentResponseDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Grade { get; set; } = string.Empty;
    }
}