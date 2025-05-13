using ForworkAcademy.Dto;
using ForworkAcademy.Models;

namespace ForworkAcademy.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(Course course);
        Task<ICollection<FullCourseDto>> GetAllCourse();
        Task<FullCourseDto> GetCourseById(int courseId);
        IEnumerable<Course> GetCourseSortedByPrice(bool orderBy);
        Task<IEnumerable<FullCourseDto>> GetCourseByCategory(int categoryId);


    }
}
