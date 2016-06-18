using System;
using System.Collections.Generic;
using Server.Entities;

namespace Server
{
    public interface IMessageStore
    {
        Response<List<Message>> GetMessage(string userId, DateTime timeLastSync);
        Response<MessageId> SendMessage(string sender, string reciever, string text);
    }
}