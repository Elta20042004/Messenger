using System;

namespace Server.Entities
{
    public class MessageResponse
    {
        public Guid MessageId { get; set; }

        public ResponseCode ResponseCode { get; set; }
    }
}