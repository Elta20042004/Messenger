using System;

namespace Server.Entities
{
    public class Message : ICloneable
    {
        public string Text { get; set; }

        public string Sender { get; set; }

        public string Reciever { get; set; }

        public int MessageNumber { get; set; }

        public DateTime TimeSpan { get; set; }

        public object Clone()
        {
            return new Message
            {
                Text = this.Text,
                Sender = this.Sender,
                Reciever = this.Reciever,
                MessageNumber = this.MessageNumber,
                TimeSpan = this.TimeSpan
            };
        }
    }
}