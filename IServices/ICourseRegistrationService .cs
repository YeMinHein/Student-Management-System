using Student_Management_System.Models;

namespace Student_Management_System.IServices
{
    public interface ICourseRegistrationService
    {
        Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync();
        Task<CourseRegistration> GetRegistrationByIdAsync(int id);
        Task AddRegistrationAsync(CourseRegistration registration);
        Task UpdateRegistrationAsync(CourseRegistration registration);
        Task DeleteRegistrationAsync(int id);
    }
}
