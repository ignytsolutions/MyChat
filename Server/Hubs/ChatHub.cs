using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyChat.Shared;

namespace MyChat.Server.Hubs {
    public class ChatHub : Hub {
        private static readonly Dictionary<string, string> userLookup = new Dictionary<string, string>();

        public async Task SendMessage(string username, string message) {
            await Clients.All.SendAsync(Messages.Receive, username, message);
        }

        public async Task Register(string username) {
            var currentId = Context.ConnectionId;

            if (!userLookup.ContainsKey(currentId)) {
                userLookup.Add(currentId, username);

                await Clients.AllExcept(currentId).SendAsync(Messages.Receive, string.Concat(username, " joined the conversation"));
            }
        }

        public override Task OnConnectedAsync() {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e) {
            string id = Context.ConnectionId;

            if (!userLookup.TryGetValue(id, out string username))
                username = "Unknown User";

            userLookup.Remove(id);

            await Clients.AllExcept(Context.ConnectionId).SendAsync(Messages.Receive, string.Concat(username, " has left the conversation"));

            await base.OnDisconnectedAsync(e);
        }
    }
}
