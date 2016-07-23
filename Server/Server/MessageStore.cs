using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.Entities;

namespace Server
{
    public class MessageStore : IMessageStore
    {
        private readonly IContactsMapping _contactsMapping;
        private readonly Dictionary<string, LinkedList<Message>> _userMessages;
        private readonly ReaderWriterLockSlim _userMessagesLock;

        public MessageStore(IContactsMapping contactsMapping)
        {
            _contactsMapping = contactsMapping;
            _userMessages = new Dictionary<string, LinkedList<Message>>();
            _userMessagesLock = new ReaderWriterLockSlim();
        }

        public Response<List<Message>> GetMessage(string userId, int lastMessageNumber)
        {
            _userMessagesLock.EnterReadLock();
            LinkedListNode<Message> lastMessage = null;
            try
            {
                if (!_userMessages.ContainsKey(userId))
                {
                    return new Response<List<Message>>(
                        new List<Message>(),
                        ResponseCode.RecieverNotFound);
                }
                lastMessage = _userMessages[userId].Last;
            }
            finally
            {
                _userMessagesLock.ExitReadLock();
            }

            var listOfLastMessages = new List<Message>();
            while (lastMessage != null
                && lastMessage.Value.MessageNumber > lastMessageNumber)
            {
                listOfLastMessages.Add(lastMessage.Value);
                lastMessage = lastMessage.Previous;
            }

            return new Response<List<Message>>(listOfLastMessages, ResponseCode.Success);
        }

        public Response<MessageId> SendMessage(string sender, string reciever, string text)
        {
            Response<MessageId> messageResponse = new Response<MessageId>();
            if (!_contactsMapping.ValidateContact(sender, reciever))
            {
                messageResponse.ErrorCode = ResponseCode.RecieverNotFound;
                return messageResponse;
            }

            AddUsersIfNotExists(sender, reciever);

            int messageNumber = AddNewMessage(sender, reciever, text);

            return new Response<MessageId>(
                new MessageId()
                {
                    MessageNumber = messageNumber
                },
                ResponseCode.Success);
        }

        private int AddNewMessage(string sender, string reciever, string text)
        {
            _userMessagesLock.EnterReadLock();
            try
            {
                var message = new Message
                {
                    TimeSpan = DateTime.Now,
                    Sender = sender,
                    Reciever = reciever,
                    Text = text,
                    MessageNumber = _userMessages[reciever].Count
                };

                _userMessages[reciever].AddLast(message);

                var senderMessage = message.Clone() as Message;
                senderMessage.MessageNumber = _userMessages[sender].Count;
                _userMessages[sender].AddLast(senderMessage);

                return senderMessage.MessageNumber;
            }
            finally
            {
                _userMessagesLock.ExitReadLock();
            }
        }

        private void AddUsersIfNotExists(string sender, string reciever)
        {
            bool recieverOk = true;
            bool senderOk = true;

            _userMessagesLock.EnterReadLock();
            try
            {
                if (!_userMessages.ContainsKey(reciever))
                {
                    recieverOk = false;
                }

                if (!_userMessages.ContainsKey(sender))
                {
                    senderOk = false;
                }
            }
            finally
            {
                _userMessagesLock.ExitReadLock();
            }

            if (!recieverOk || !senderOk)
            {
                _userMessagesLock.EnterWriteLock();
                try
                {
                    if (!recieverOk)
                    {
                        _userMessages.Add(reciever, new LinkedList<Message>());
                    }
                    if (!senderOk)
                    {
                        _userMessages.Add(sender, new LinkedList<Message>());
                    }
                }
                finally
                {
                    _userMessagesLock.ExitWriteLock();
                }
            }
        }
    }
}
