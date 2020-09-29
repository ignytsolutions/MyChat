using System;

namespace MyChat.Shared {
    public class MessageReceivedEventArgs : EventArgs {
        public string Username { get; set; }
        public string Message { get; set; }

        public MessageReceivedEventArgs(string username, string message) {
            this.Username = username;
            this.Message = message;
        }
    }
}
