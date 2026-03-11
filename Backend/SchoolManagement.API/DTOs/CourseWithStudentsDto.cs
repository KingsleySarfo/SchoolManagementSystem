namespace SchoolManagement.API.DTOs
{
    public class CourseWithStudentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<StudentDto> Students { get; set; } = new();
    }

    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}