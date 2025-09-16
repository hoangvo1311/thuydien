using IndusG.Web.Hubs;
using Microsoft.AspNet.SignalR;
using System.Net.Http;
using System.Web.Http;

namespace IndusG.Web.Controllers
{
    public class NotificationController : ApiController
    {
        public HttpResponseMessage PostMessage(string message)
        {
            NotificationHub.Send(message);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [Route("api/Notification/NotifyServiceStopped")]
        public HttpResponseMessage NotifyServiceStopped()
        {
            NotificationHub.NotifyServiceStopped();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [Route("api/Notification/NotifyServiceRunning")]
        public HttpResponseMessage NotifyServiceRunning()
        {
            NotificationHub.NotifyServiceRunning();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [Route("api/Notification/NotifyPLCDisconnect")]
        public HttpResponseMessage NotifyPLCDisconnect()
        {
            NotificationHub.NotifyPLCDisconnect();
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
