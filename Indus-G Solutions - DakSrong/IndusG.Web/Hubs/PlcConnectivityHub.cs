using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndusG.Web.Hubs
{
    public class NotificationHub : Hub
    {
        private static IHubContext hubContext =
    GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

        public static void Send(string message)
        {
            hubContext.Clients.All.acknowledgeMessage(message);
        }

        public static void NotifyServiceStopped()
        {
            hubContext.Clients.All.notifyServiceStopped();
        }

        public static void NotifyServiceRunning()
        {
            hubContext.Clients.All.notifyServiceRunning();
        }

        public static void NotifyPLCDisconnect()
        {
            hubContext.Clients.All.notifyPLCDisconnect();
        }
    }
}