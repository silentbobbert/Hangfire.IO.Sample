using Microsoft.AspNet.SignalR;
using System;

namespace Hangfire.IO.Sample.BusinessLogic.Hubs
{
    public class NotificationHub : Hub
    {
        private static readonly Lazy<NotificationHub> LazyHub = new Lazy<NotificationHub>();
        public static NotificationHub Instance {
            get
            {
                return LazyHub.Value;
            }
        }

        private readonly IHubContext _hubContext;

        public NotificationHub()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        }
        public void Notify(string message)
        {
            _hubContext.Clients.All.Notify(message);
        }
    }
}