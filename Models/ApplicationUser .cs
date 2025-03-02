using Microsoft.AspNetCore.Identity;

namespace Student_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
