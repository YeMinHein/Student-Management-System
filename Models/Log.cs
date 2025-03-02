namespace Student_Management_System.Models
{
    public class Log
    {
        public int Id { get; set; } // Primary Key
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Log time
        public string LogLevel { get; set; } // Info, Warning, Error
        public string Message { get; set; } // Log message
        public string Exception { get; set; } // Exception details (if any)
        public string StackTrace { get; set; } // StackTrace for debugging
    }
}
