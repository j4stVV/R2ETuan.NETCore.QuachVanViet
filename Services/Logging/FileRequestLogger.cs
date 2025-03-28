using R2ETuan.NETCore.QuachVanViet.Configuration;
using System.Text;

namespace R2ETuan.NETCore.QuachVanViet.Services.Logging
{
    public class FileRequestLogger : IRequestLogger
    {
        private readonly ILogger<FileRequestLogger> _logger;
        private readonly string _logFilePath;

        public FileRequestLogger(ILogger<FileRequestLogger> logger, ILoggingOptions options)
        {
            _logger = logger;
            _logFilePath = options.LogFilePath;
        }
        public async Task LogRequestAsync(string scheme, string host, string path, string queryString, string requestBody)
        {
            var logEntry = new StringBuilder();
            logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Request Log: ");
            logEntry.AppendLine($"Scheme: {scheme}");
            logEntry.AppendLine($"Host: {host}");
            logEntry.AppendLine($"Path: {path}");
            logEntry.AppendLine($"Query string: {queryString}");
            logEntry.AppendLine($"Request body: {requestBody}");
            logEntry.AppendLine("------------------------------");

            try
            {
                await File.AppendAllTextAsync(_logFilePath, logEntry.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing to log file");
            }
        }
    }
}
