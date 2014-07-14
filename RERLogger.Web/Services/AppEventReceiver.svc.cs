using System;
using System.ServiceModel;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using RERLogger.Web.SharePoint;

namespace RERLogger.Web.Services
{
    public class AppEventReceiver : IRemoteEventService
    {
        private ClientContext _appWebContext;

        /// <summary>
        /// Handles app events that occur after the app is installed or upgraded, or when app is being uninstalled.
        /// </summary>
        /// <param name="properties">Holds information about the app event.</param>
        /// <returns>Holds information returned from the app event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            _appWebContext = TokenHelper.CreateAppEventClientContext(properties, useAppWeb: true);

            var result = new SPRemoteEventResult();


            IEventReceiverHandler schema = new EventReceiverHandler(_appWebContext);

            switch (properties.EventType)
            {
                    case SPRemoteEventType.AppInstalled:
                        schema.AddReceiver("General", EventReceiverType.ItemAdded, ServiceEndPoint);
                        schema.AddReceiver("General", EventReceiverType.ItemAdding, ServiceEndPoint);
                        schema.AddReceiver("General", EventReceiverType.ItemUpdating, ServiceEndPoint);
                        schema.AddReceiver("General", EventReceiverType.ItemUpdated, ServiceEndPoint);
                    break;
            }

            return result;
        }

        public string ServiceEndPoint
        {
            get
            {
                var url = OperationContext.Current.IncomingMessageHeaders.To.ToString();
                return url.Replace("App", "LogItem");
            }
        }

        /// <summary>
        /// This method is a required placeholder, but is not used by app events.
        /// </summary>
        /// <param name="properties">Unused.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            throw new NotImplementedException();
        }

    }
}
