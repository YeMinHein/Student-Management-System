using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public bool Isactived { get; set; } = true;

        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
              
    }
}
