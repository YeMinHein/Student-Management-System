namespace Student_Management_System.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string Month { get; set; }
        public double AttendancePercentage { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
