namespace Student_Management_System.IServices
{
    public interface ILogService
    {
        Task LogAsync(string logLevel, string message, string exception = null, string stackTrace = null);
    }
}
