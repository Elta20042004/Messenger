﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Server.Entities;

namespace Server
{
    public class MessageController : ApiController
    {
        private readonly IMessageStore _messageStore;
        public MessageController()
        {
            _messageStore =
                ServiceLocator.Current.GetInstance<IMessageStore>();
        }

        // GET http://localhost:9000/api/message?userId=Moshe&lastMessageNumber=-1
        public Response<List<Message>> Get(string userId, int lastMessageNumber)
        {
            Console.WriteLine(this.Request.RequestUri);
            var lastMessages = _messageStore.GetMessage(userId, lastMessageNumber);
            return lastMessages;
        }

        // POST http://localhost:9000/api/message?sender=Moshe&reciever=Avi&text=Hello
        public Response<MessageId> Post(string sender, string reciever, string text)
        {
            Console.WriteLine(this.Request.RequestUri);
            Response<MessageId> answer = _messageStore.SendMessage(sender, reciever, text);
            return answer;
        }  
    }
}
