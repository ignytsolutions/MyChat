using System;

namespace MyChat.Shared {
    public class Message {
        public string CSS { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
        public bool Mine { get; set; }
        public DateTime MessageDateTime { get; set; }

        public Message(string username, string body, bool mine) {
            this.Username = username;
            this.Body = body;
            this.MessageDateTime = DateTime.Now;
            this.Mine = mine;
            this.CSS = Mine ? "sent" : "received";
        }

        public override string ToString() {
            return string.Concat(this.Username, "   ", this.MessageDateTime.ToString("HH:mm"));
        }
    }
}
