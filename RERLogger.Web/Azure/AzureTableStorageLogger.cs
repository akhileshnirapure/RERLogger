using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace RERLogger.Web.Azure
{
    public class AzureTableStorageLogger : ILogger
    {
        private readonly CloudTable _loggingTable;

        public AzureTableStorageLogger() : this("rerloggerdev")
        {
        }

        public AzureTableStorageLogger(string connectionName)
        {
            var cloudStorage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionName));
            var cloudTableClient = cloudStorage.CreateCloudTableClient();
            _loggingTable = cloudTableClient.GetTableReference("Logging");
            _loggingTable.CreateIfNotExists();
        }

        public void LogMessage(string eventType,string message)
        {
            var msg = new LoggingEntry { EventType = eventType, Message = message};
            var operation = TableOperation.Insert(msg);
            _loggingTable.Execute(operation);
        }

        public void LogVerbose(string message)
        {
            LogMessage("Verbose",message);
        }

        internal class LoggingEntry : TableEntity
        {
            public LoggingEntry()
            {
                PartitionKey = "RER";
                //RowKey = string.Format("{0}_{1}",type,DateTime.UtcNow.ToString("O"));
                RowKey = Guid.NewGuid().ToString("N");

            }

            public string EventType { get; set; }
            public string Message { get; set; }

        }
    }
}