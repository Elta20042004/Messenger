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

        public List<Message> GetMessage(string userId, DateTime timeLastSync)
        {
            _userMessagesLock.EnterReadLock();
            LinkedListNode<Message> lastMessage = null;
            try
            {
                if (!_userMessages.ContainsKey(userId))
                {
                    return new List<Message>();
                }
                lastMessage = _userMessages[userId].Last;
            }
            finally
            {
                _userMessagesLock.ExitReadLock();
            }

            var listOfLastMessages = new List<Message>();
            while (lastMessage != null
                && lastMessage.Value.TimeSpan > timeLastSync)
            {
                listOfLastMessages.Add(lastMessage.Value);
                lastMessage = lastMessage.Previous;
            }

            return listOfLastMessages;
        }

        public MessageResponse SendMessage(string sender, string reciever, string text)
        {
            MessageResponse messageResponse = new MessageResponse();
            if (!_contactsMapping.ValidateContact(sender, reciever))
            {
                messageResponse.ResponseCode = ResponseCode.RecieverNotFound;
                return messageResponse;
            }

            AddUsersIfNotExists(sender, reciever);

            var message = new Message
            {
                TimeSpan = DateTime.Now,
                Sender = sender,
                Reciever = reciever,
                Text = text,
                Id = Guid.NewGuid()
            };

            AddNewMessage(sender, reciever, message);

            messageResponse.ResponseCode = ResponseCode.Success;
            messageResponse.MessageId = message.Id;
            return messageResponse;
        }

        private void AddNewMessage(string sender, string reciever, Message k)
        {
            _userMessagesLock.EnterReadLock();
            try
            {
                _userMessages[reciever].AddLast(k);
                _userMessages[sender].AddLast(k);
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
