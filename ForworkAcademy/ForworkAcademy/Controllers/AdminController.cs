using ForworkAcademy.Data;
using ForworkAcademy.Dto;
using ForworkAcademy.ExceptionHandling;
using ForworkAcademy.Interfaces;
using ForworkAcademy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForworkAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ForworkDbContext _dbContext;
        private readonly ICourseRepository _courseRepository;

        public AdminController(ForworkDbContext dbContext, ICourseRepository courseRepository)
        {
            _dbContext = dbContext;
            _courseRepository = courseRepository;
        }

        [HttpPost("admin-login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.UserName == loginDto.UserName)??
                throw new CourseNotFoundException("veripova");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
                return Unauthorized("Invalid credentials");

            return Ok("Login successful");
        }

            [HttpPost]
            public async Task<IActionResult> AddCourse(Course course)
            {
                await _courseRepository.AddCourse(course);
                return new JsonResult(course);
            }
    }
}
