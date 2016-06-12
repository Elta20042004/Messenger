using System;
using System.Collections.Generic;
using Server.Entities;

namespace Server
{
    public interface IMessageStore
    {
        List<Message> GetMessage(string userId, DateTime timeLastSync);
        MessageResponse SendMessage(string sender, string reciever, string text);
    }
}