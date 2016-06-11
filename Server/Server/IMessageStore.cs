using System;
using System.Collections.Generic;

namespace Server
{
    public interface IMessageStore
    {
        List<Message> GetMessage(string userId, DateTime timeLastSync);
        ResponseCode SendMessage(string sender, string reciever, string text);
    }
}