using System.Text.Json.Serialization;

namespace ForworkAcademy.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public List<Course> Courses{ get; set; }
    }
}
