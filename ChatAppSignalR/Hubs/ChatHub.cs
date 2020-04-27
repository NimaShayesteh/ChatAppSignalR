using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppSignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.Name == null)
                return;

            await Clients.Caller.SendAsync("RecieveMessage", "Supportment Part", DateTimeOffset.UtcNow, "Welcome to our chat application!");
            var name = Context.User.Identity.Name;
            _connections.Add(name, Context.ConnectionId);
            var allConnections = _connections.GetAllConnections();
            
            await Clients.All.SendAsync("ShowContactList", "Supportment Part", DateTimeOffset.UtcNow,allConnections);
            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;
            _connections.Remove(name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string user, string message)
        {
            var messageObj = new Models.MessageModel { Name = user, text = message, sendAt = DateTimeOffset.UtcNow };
            string Name = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(user))
            {
                await Clients.Client(connectionId).SendAsync("RecieveMessage", messageObj.Name, messageObj.text, messageObj.sendAt);
            }

         //   await Clients.All.SendAsync("RecieveMessage", messageObj.Name , messageObj.text , messageObj.sendAt);
        }

        public  Task SendPrivateMessage(string user, string message)
        {
            return Clients.User("sina").SendAsync("RecieveMessage", message);
        }
    }
}
