using ForworkAcademy.Data;
using ForworkAcademy.Dto;
using ForworkAcademy.ExceptionHandling;
using ForworkAcademy.Interfaces;
using ForworkAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForworkAcademy.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ForworkDbContext _forworkDbContext;

        public CourseRepository(ForworkDbContext forworkDbContext)
        {
            _forworkDbContext = forworkDbContext;
        }
        public async Task<Course> AddCourse(Course course)
        {
            var courses = new Course
            {
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Price = course.Price,
                CategoryId = course.CategoryId,
                LecturerId = course.LecturerId,

            };
            await _forworkDbContext.AddAsync(courses);
            await _forworkDbContext.SaveChangesAsync();
            return courses;
        }

        public async Task<ICollection<FullCourseDto>> GetAllCourse()
        {
            var result = await (from course in _forworkDbContext.Course
                                join category in _forworkDbContext.Category
                                on course.CategoryId equals category.Id
                                select new FullCourseDto
                                {
                                    Id = course.Id,
                                    CourseName = course.CourseName,
                                    CategoryName = category.CategoryName,
                                    CourseDescription = course.CourseDescription,
                                    Price = course.Price
                                }).ToListAsync();
            if (result == null)
                throw new CourseNotFoundException("Course Not Found");
            else
                return result;
        }



        public async Task<FullCourseDto> GetCourseById(int courseId)
        {
            /*var result = await (from course in _forworkDbContext.Course
                                join category in _forworkDbContext.Category
                                    on course.CategoryId equals category.Id
                                join lecturer in _forworkDbContext.Lecturer
                                    on course.LecturerId equals lecturer.Id
                                where course.Id == courseId
                                select new FullCourseDto
                                {
                                    Id = course.Id,
                                    CourseName = course.CourseName,
                                    CategoryName = category.CategoryName,
                                    CourseDescription = course.CourseDescription,
                                    Price = course.Price,
                                    LecturerName = lecturer.Name,
                                    LecturerLastName = lecturer.LastName,
                                    LecturerDescription = lecturer.Description
                                }).FirstOrDefaultAsync();*/

            var course = await _forworkDbContext.Course.Include(c => c.Category).Include(c => c.Lecturer)
                .FirstOrDefaultAsync(c => c.Id == courseId);
            if (course== null)
                throw new CourseNotFoundException("Course Not Found");

            var result = new FullCourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                CategoryName = course.Category.CategoryName,
                CourseDescription = course.CourseDescription,
                Price = course.Price,
                LecturerName = course.Lecturer.Name,
                LecturerLastName = course.Lecturer.LastName,
                LecturerDescription = course.Lecturer.Description
            };
            return result;
        }



        public IEnumerable<Course> GetCourseSortedByPrice(bool orderBy)
        {
            var course = _forworkDbContext.Course;
            if (orderBy)
                return course.OrderByDescending(p => p.Price).ToList();
            else
                return course.OrderBy(p => p.Price).ToList();
        }

        public async Task<IEnumerable<FullCourseDto>> GetCourseByCategory(int categoryId)
        {
            var result = await (from course in _forworkDbContext.Course
                                join category in _forworkDbContext.Category
                                on course.CategoryId equals category.Id
                                where course.CategoryId == categoryId
                                select new FullCourseDto
                                {
                                    Id = course.Id,
                                    CourseName = course.CourseName,
                                    CategoryName = category.CategoryName,
                                    CourseDescription = course.CourseDescription,
                                    Price = course.Price
                                }).ToListAsync();

            if (result == null)
                throw new CourseNotFoundException("Course Not Found");
            else
                return result;
        }

    }
}
