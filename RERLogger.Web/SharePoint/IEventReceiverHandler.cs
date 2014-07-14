using Microsoft.SharePoint.Client;

namespace RERLogger.Web.SharePoint
{
    public interface IEventReceiverHandler
    {
        void AddReceiver(string listName,EventReceiverType type,string url);
    }
}