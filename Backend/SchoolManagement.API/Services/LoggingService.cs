namespace SchoolManagement.API.Services
{
    public class LoggingService
    {
        public void Log(string message)
        {
            var logMessage = $"{DateTime.Now} - {message}";
            Console.WriteLine(logMessage);

            File.AppendAllText("logs.txt", logMessage + Environment.NewLine);
        }
    }
}