namespace RERLogger.Web.Azure
{
    public interface ILogger
    {
        void LogMessage(string eventType,string message);
        void LogVerbose(string message);
    }
}