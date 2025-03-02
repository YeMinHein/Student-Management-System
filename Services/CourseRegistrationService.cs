using Student_Management_System.IRepositories;
using Student_Management_System.IServices;
using Student_Management_System.Models;

namespace Student_Management_System.Services
{
    public class CourseRegistrationService : ICourseRegistrationService
    {
        private readonly ICourseRegistrationRepository _repository;

        public CourseRegistrationService(ICourseRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CourseRegistration> GetRegistrationByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddRegistrationAsync(CourseRegistration registration)
        {
            await _repository.AddAsync(registration);
        }

        public async Task UpdateRegistrationAsync(CourseRegistration registration)
        {
            await _repository.UpdateAsync(registration);
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
