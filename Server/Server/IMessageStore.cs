using System;
using System.Collections.Generic;
using Server.Entities;

namespace Server
{
    public interface IMessageStore
    {
        Response<List<Message>> GetMessage(string userId, int lastMessageNumber);

        Response<MessageId> SendMessage(string sender, string reciever, string text);
    }
}