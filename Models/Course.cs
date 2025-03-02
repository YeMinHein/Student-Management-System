using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; }

        public string Description { get; set; }

        [Required]
        public int CreditHours { get; set; }

        [Required]
        public bool Isactived { get; set; } = true;

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
