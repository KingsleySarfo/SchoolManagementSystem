using SchoolManagement.API.Models;
using System.Text.Json.Serialization;

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? TeacherId { get; set; }

    [JsonIgnore]
    public Teacher? Teacher { get; set; }

    [JsonIgnore]
    public ICollection<Student> Students { get; set; } = new List<Student>();
}