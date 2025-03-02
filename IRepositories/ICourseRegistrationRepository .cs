using Student_Management_System.Models;

namespace Student_Management_System.IRepositories
{
    public interface ICourseRegistrationRepository
    {
        Task<IEnumerable<CourseRegistration>> GetAllAsync();
        Task<CourseRegistration> GetByIdAsync(int id);
        Task AddAsync(CourseRegistration registration);
        Task UpdateAsync(CourseRegistration registration);
        Task DeleteAsync(int id);
    }
}
