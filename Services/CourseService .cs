
using Student_Management_System.IRepositories;
using Student_Management_System.IServices;
using Student_Management_System.Models;

namespace Student_Management_System.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepossitory _courseRopository;

        public CourseService(ICourseRepossitory courseRopository)
        {
            _courseRopository = courseRopository;
        }

        public async Task AddCourseAsync(Course course)
        {
            await _courseRopository.AddCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRopository.DeleteCourseAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
           return await _courseRopository.GetAllCoursesAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _courseRopository.GetCourseByIdAsync(id);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await _courseRopository.UpdateCourseAsync(course);
        }
    }
}
