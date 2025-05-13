using System.Text.Json.Serialization;

namespace ForworkAcademy.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LecturerId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public string CourseDescription { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonIgnore]
        public Lecturer? Lecturer { get; set; }


    }
}
