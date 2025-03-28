namespace R2ETuan.NETCore.QuachVanViet.Configuration
{
    public class LoggingOptions : ILoggingOptions
    {
        public string LogFilePath { get; set; } = "requestlog.txt";
    }
}
