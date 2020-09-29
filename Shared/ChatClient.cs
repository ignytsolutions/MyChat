using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace MyChat.Shared {
    public class ChatClient : IAsyncDisposable {
        public const string HUBURL = "/ChatHub";
        private readonly string _hubUrl;
        private readonly string _username;
        private HubConnection _hubConnection;

        public event MessageReceivedEventHandler MessageReceived;
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

        public ChatClient(string username, string siteUrl) {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(siteUrl))
                throw new ArgumentNullException(nameof(siteUrl));

            this._username = username;
            this._hubUrl = siteUrl.TrimEnd('/') + HUBURL;
        }

        public async Task StartAsync() {
            if (this._hubConnection == null) {
                this._hubConnection = new HubConnectionBuilder().WithUrl(_hubUrl).Build();
                this._hubConnection.On<string, string>(Messages.Receive, (user, message) => { HandleReceiveMessage(user, message); });

                await this._hubConnection.StartAsync();

                await this._hubConnection.SendAsync(Messages.Register, string.Concat(_username," has joined the conversation"));
            }
        }

        private void HandleReceiveMessage(string username, string message) {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }

        public async Task SendAsync(string message) {
            if (this._hubConnection == null && this._hubConnection.State != HubConnectionState.Connected)
                throw new InvalidOperationException("Client has not started");

            await _hubConnection.SendAsync(Messages.Send, _username, message);
        }

        public async Task StopAsync() {
            if (this._hubConnection != null && this._hubConnection.State == HubConnectionState.Connected)
                await _hubConnection.StopAsync();
        }

        public async ValueTask DisposeAsync() {
            if (this._hubConnection != null && this._hubConnection.State == HubConnectionState.Connected)
                await this._hubConnection.DisposeAsync();
        }
    }
}
