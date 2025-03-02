using Student_Management_System.Data;
using Student_Management_System.IServices;
using Student_Management_System.Models;

namespace Student_Management_System.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string logLevel, string message, string exception, string stackTrace)
        {
            var log = new Log
            {
                LogLevel = logLevel,
                Message = message,
                Exception = exception == null ? message : exception,
                StackTrace = stackTrace == null ? message : stackTrace,
                Timestamp = DateTime.UtcNow
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
