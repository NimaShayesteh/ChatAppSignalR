using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppSignalR.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string user, string message)
        {
            var messageObj = new Models.MessageModel { Name = user, text = message, sendAt = DateTimeOffset.UtcNow };
            await Clients.All.SendAsync("RecieveMessage", messageObj.Name , messageObj.text , messageObj.sendAt);

        }
    }
}
