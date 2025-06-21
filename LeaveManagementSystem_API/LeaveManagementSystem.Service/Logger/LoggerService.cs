using Microsoft.Extensions.Configuration;
using System.Text;

namespace LeaveManagementSystem.Service.Logger
{
    public class LoggerService
    {
        private readonly IConfiguration _config;

        public LoggerService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> LogResponseAsync(string response, string logFileName)
        {
            try
            {
                var logFolderPath = _config["PaytmPaymentGatway:PaymentLogsPath"];
                var logFilePath = GetLogFilePath(logFolderPath, $"{DateTime.Now:yyyy-MM-dd}-{logFileName}.txt");

                await WriteToLogFileAsync(logFilePath, response);
                return true;
            }
            catch (Exception ex)
            {
                // Handle or log any exceptions if needed
                Console.WriteLine($"Error logging response: {ex.Message}");
                return false;
            }
        }

        private string GetLogFilePath(string logFolderPath, string logFileName)
        {
            var baseUrl = Directory.GetCurrentDirectory();
            var path2 = baseUrl + logFolderPath; // Assuming current directory
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }

            return Path.Combine(path2, logFileName);
        }

        private async Task WriteToLogFileAsync(string logFilePath, string response)
        {
            using (var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                var logMessage = $"{DateTime.Now:s}: {response}" + Environment.NewLine + "-----------------------------------------------------------------" + Environment.NewLine;
                byte[] logBytes = Encoding.UTF8.GetBytes(logMessage);
                await fileStream.WriteAsync(logBytes, 0, logBytes.Length);
            }
        }
    }
}