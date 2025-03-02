using Moq;
using Student_Management_System.IRepositories;
using Student_Management_System.Models;
using Student_Management_System.Services;

namespace StudentManagementSystem.Tests.Services
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _studentService = new StudentService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllStudentsAsync_ReturnsStudents()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Mg Mg", Email = "mgmg@example.com",DOB=DateTime.Now},
                new Student { Id = 2, Name = "Hla Hla", Email = "hla@example.com",DOB=DateTime.Now}
            };

            _mockRepo.Setup(repo => repo.GetAllStudentsAsync()).ReturnsAsync(students);

            // Act
            var result = await _studentService.GetAllStudentsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetStudentByIdAsync_ExistingId_ReturnsStudent()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "Mg Mg", Email = "mgmg@example.com",DOB = DateTime.Now };

            _mockRepo.Setup(repo => repo.GetStudentByIdAsync(1)).ReturnsAsync(student);

            // Act
            var result = await _studentService.GetStudentByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Mg Mg", result.Name);
        }

       [Fact]
        public async Task GetStudentByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetStudentByIdAsync(99)).ReturnsAsync((Student)null);

            // Act
            var result = await _studentService.GetStudentByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddStudentAsync_ValidStudent_CallsRepositoryOnce()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "Mg Mg", Email = "mgmg@example.com", DOB = DateTime.Now };

            _mockRepo.Setup(repo => repo.AddStudentAsync(student)).Returns(Task.CompletedTask);

            // Act
            await _studentService.AddStudentAsync(student);

            // Assert
            _mockRepo.Verify(repo => repo.AddStudentAsync(student), Times.Once);
        }

        [Fact]
        public async Task UpdateStudentAsync_ValidStudent_CallsRepositoryOnce()
        {
            // Arrange
            var student = new Student { Id = 1, Name = "Mg Mule", Email = "mgmg@example.com",DOB = DateTime.Now };

            _mockRepo.Setup(repo => repo.UpdateStudentAsync(student)).Returns(Task.CompletedTask);

            // Act
            await _studentService.UpdateStudentAsync(student);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateStudentAsync(student), Times.Once);
        }

        [Fact]
        public async Task DeleteStudentAsync_ValidId_CallsRepositoryOnce()
        {
            // Arrange
            var studentId = 1;

            _mockRepo.Setup(repo => repo.DeleteStudentAsync(studentId)).Returns(Task.CompletedTask);

            // Act
            await _studentService.DeleteStudentAsync(studentId);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteStudentAsync(studentId), Times.Once);
        }
    }
}
