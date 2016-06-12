using System;
using System.Data;

namespace Server
{
    public class Message
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public DateTime TimeSpan { get; set; }
    }
}