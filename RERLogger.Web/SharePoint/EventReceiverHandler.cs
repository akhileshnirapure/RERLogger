using System;
using Microsoft.SharePoint.Client;

namespace RERLogger.Web.SharePoint
{
    public class EventReceiverHandler : IEventReceiverHandler 
    {
        private readonly ClientContext _context;

        public EventReceiverHandler(ClientContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }


        public void AddReceiver(string listName,EventReceiverType type,string remoteUrl)
        {

            if (IsValidEvent(type) == false)
            {
                return;
            }

            var def = new EventReceiverDefinitionCreationInformation();
            def.EventType = type;
            def.ReceiverName = string.Format("Test_{0}", type);
            def.ReceiverClass = null;
            def.ReceiverUrl = remoteUrl;
            def.SequenceNumber = GetSequence(type);
            def.Synchronization = GetSynchronnization(type);

            var list = _context.Web.Lists.GetByTitle(listName);
            list.EventReceivers.Add(def);
            _context.ExecuteQuery();
        }

        private static bool IsValidEvent(EventReceiverType type)
        {
            switch (type)
            {
                // Un-Comment events as required.
                case EventReceiverType.ItemAdding:
                case EventReceiverType.ItemAdded:
                case EventReceiverType.ItemUpdating:
                case EventReceiverType.ItemUpdated:
                case EventReceiverType.ItemDeleting:
                case EventReceiverType.ItemDeleted:
                    return true;
                //case EventReceiverType.ItemCheckingIn:
                //    break;
                //case EventReceiverType.ItemCheckingOut:
                //    break;
                //case EventReceiverType.ItemUncheckingOut:
                //    break;
                //case EventReceiverType.ItemFileMoving:
                //    break;
                //case EventReceiverType.ItemVersionDeleting:
                //    break;


                //case EventReceiverType.ItemDeleted:
                //    break;
                //case EventReceiverType.ItemCheckedIn:
                //    break;
                //case EventReceiverType.ItemCheckedOut:
                //    break;
                //case EventReceiverType.ItemUncheckedOut:
                //    break;
                default:
                    return false;

            }
        }

        private static EventReceiverSynchronization GetSynchronnization(EventReceiverType type)
        {
            switch (type)
            {
                // Un-Comment events as required.

                case EventReceiverType.ItemAdding:
                    return EventReceiverSynchronization.Synchronous;
                case EventReceiverType.ItemAdded:
                    return EventReceiverSynchronization.Asynchronous;
                case EventReceiverType.ItemUpdating:
                    return EventReceiverSynchronization.Synchronous;
                case EventReceiverType.ItemUpdated:
                    return EventReceiverSynchronization.Asynchronous;
                case EventReceiverType.ItemDeleting:
                    return EventReceiverSynchronization.Synchronous;
                case EventReceiverType.ItemDeleted:
                    return EventReceiverSynchronization.Asynchronous;
                //case EventReceiverType.ItemCheckingIn:
                //    break;
                //case EventReceiverType.ItemCheckingOut:
                //    break;
                //case EventReceiverType.ItemUncheckingOut:
                //    break;
                //case EventReceiverType.ItemFileMoving:
                //    break;
                //case EventReceiverType.ItemVersionDeleting:
                //    break;


                //case EventReceiverType.ItemDeleted:
                //    break;
                //case EventReceiverType.ItemCheckedIn:
                //    break;
                //case EventReceiverType.ItemCheckedOut:
                //    break;
                //case EventReceiverType.ItemUncheckedOut:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException("type");

            }
        }

        private static int GetSequence(EventReceiverType type)
        {
            var seq = 10000;
            switch (type)
            {
                // Un-Comment events as required.

                case EventReceiverType.ItemAdding:
                    seq = seq + 5;
                    break;
                case EventReceiverType.ItemAdded:
                    seq = seq + 6;
                    break;
                case EventReceiverType.ItemUpdating:
                    seq = seq + 10;
                    break;
                case EventReceiverType.ItemUpdated:
                    seq = seq + 11;
                    break;
                case EventReceiverType.ItemDeleting:
                    seq = seq + 15;
                    break;
                case EventReceiverType.ItemDeleted:
                    seq = seq + 16;
                    break;
                //case EventReceiverType.ItemCheckingIn:
                //    break;
                //case EventReceiverType.ItemCheckingOut:
                //    break;
                //case EventReceiverType.ItemUncheckingOut:
                //    break;
                //case EventReceiverType.ItemFileMoving:
                //    break;
                //case EventReceiverType.ItemVersionDeleting:
                //    break;
                
                
                //case EventReceiverType.ItemDeleted:
                //    break;
                //case EventReceiverType.ItemCheckedIn:
                //    break;
                //case EventReceiverType.ItemCheckedOut:
                //    break;
                //case EventReceiverType.ItemUncheckedOut:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException("type");

            }
            return seq;
        }
    }
}