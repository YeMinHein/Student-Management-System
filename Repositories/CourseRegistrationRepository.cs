using Microsoft.EntityFrameworkCore;
using Student_Management_System.Data;
using Student_Management_System.IRepositories;
using Student_Management_System.Models;

namespace Student_Management_System.Repositories
{
    public class CourseRegistrationRepository : ICourseRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseRegistration>> GetAllAsync()
        {
            return await _context.CourseRegistrations
                .Include(cr => cr.Student)
                .Include(cr => cr.Course)
                .ToListAsync();
        }

        public async Task<CourseRegistration> GetByIdAsync(int id)
        {
            return await _context.CourseRegistrations
                .Include(cr => cr.Student)
                .Include(cr => cr.Course)
                .FirstOrDefaultAsync(cr => cr.Id == id);
        }

        public async Task AddAsync(CourseRegistration registration)
        {
            await _context.CourseRegistrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseRegistration registration)
        {
            _context.CourseRegistrations.Update(registration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var registration = await _context.CourseRegistrations.FindAsync(id);
            if (registration != null)
            {
                _context.CourseRegistrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
    }
}
