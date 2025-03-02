using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;

namespace Student_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(cr => cr.Student)
                .WithMany()
                .HasForeignKey(cr => cr.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(cr => cr.Course)
                .WithMany()
                .HasForeignKey(cr => cr.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }*/
    }
}
