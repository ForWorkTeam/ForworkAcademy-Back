using ForworkAcademy.Dto;
using ForworkAcademy.Interfaces;
using ForworkAcademy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForworkAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForworkController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public ForworkController(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }



        [HttpGet("get-all-course")]
        public async Task<IActionResult> GetAllCourse()
        {
            var courses = await _courseRepository.GetAllCourse();

            return Ok(courses);
        }
        [HttpGet("course-by-id")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var course = await _courseRepository.GetCourseById(courseId);
            return Ok(course);
        }

        [HttpGet("order-by-price")]
        public IEnumerable<Course> OrderCourseByPrice(string orderBy)
        {
            bool ascending = orderBy.ToUpper() == "asc";

            var courseByPrice = _courseRepository.GetCourseSortedByPrice(ascending);
            return courseByPrice;
        }

        [HttpGet("course-by-category")]
        public async Task<IEnumerable<FullCourseDto>> GetCourseByCategory(int categoryId)
        {
            var courses = await _courseRepository.GetCourseByCategory(categoryId);
            return courses;
        }


        [HttpPost("send-popup")]
        public async Task<IActionResult> SendPopup(UserPopup userPopup)
        {
            var popup = await _userRepository.SendUserPopup(userPopup);
            return Ok(popup);

        }
    }
}
