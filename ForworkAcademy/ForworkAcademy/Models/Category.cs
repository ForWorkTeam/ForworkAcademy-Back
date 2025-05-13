using System.Text.Json.Serialization;

namespace ForworkAcademy.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public List<Course> Courses { get; set; }
    }
}
