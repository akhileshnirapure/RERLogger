using Microsoft.SharePoint.Client.EventReceivers;
using Newtonsoft.Json;
using RERLogger.Web.Azure;

namespace RERLogger.Web.Services
{
    public class LogItemEventReceiver : IRemoteEventService
    {
        private readonly ILogger _logger;

        public LogItemEventReceiver()
        {
            _logger = new AzureTableStorageLogger();
        }

        /// <summary>
        /// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
        /// </summary>
        /// <param name="properties">Ho2122lds information about the app event.</param>
        /// <returns>Holds information returned from the app event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            var result = new SPRemoteEventResult();
            //_logger.LogMessage(properties.EventType.ToString(),"Sync Event Start");

            switch (properties.EventType)
            {
                    case SPRemoteEventType.ItemAdding:
                    case SPRemoteEventType.ItemUpdating:
                    var msg = new EventMessage
                    {
                        Event = properties.EventType.ToString(),
                        RemoteItemProperties = properties.ItemEventProperties
                    };
                        _logger.LogMessage(properties.EventType.ToString(),JsonConvert.SerializeObject(msg));
                    break;
            }
            //_logger.LogMessage(properties.EventType.ToString(), "Sync Event End");

            return result;
        }

        /// <summary>
        /// This method is a required placeholder, but is not used by app events.
        /// </summary>
        /// <param name="properties">Unused.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            //_logger.LogMessage(properties.EventType.ToString(), "Async Event Start");
            switch (properties.EventType)
            {
                case SPRemoteEventType.ItemAdded:
                case SPRemoteEventType.ItemUpdated:
                    //_logger.LogMessage(properties.EventType.ToString(), JsonConvert.SerializeObject(properties.ItemEventProperties));
                    var msg = new EventMessage
                    {
                        Event = properties.EventType.ToString(),
                        RemoteItemProperties = properties.ItemEventProperties
                    };
                        _logger.LogMessage(properties.EventType.ToString(),JsonConvert.SerializeObject(msg));
                    break;
            }
            //_logger.LogMessage(properties.EventType.ToString(), "Async Event Start");
        }

        class EventMessage
        {
            public string Event { get; set; }
            public SPRemoteItemEventProperties RemoteItemProperties { get; set; }
        }

    }

}
