using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MessageStore : IMessageStore
    {
        private readonly IContactsMapping _contactsMapping;
        private readonly Dictionary<string, LinkedList<Message>> _userMessages;

        public MessageStore(IContactsMapping contactsMapping)
        {
            _contactsMapping = contactsMapping;
            _userMessages = new Dictionary<string, LinkedList<Message>>();
        }

        public List<Message> GetMessage(string userId, DateTime timeLastSync)
        {
            List<Message> listOfLastMessages = new List<Message>();

            if (!_userMessages.ContainsKey(userId))
            {
                return listOfLastMessages;
            }

            LinkedListNode<Message> lastMessage = _userMessages[userId].Last;

            while (lastMessage != null
                && lastMessage.Value.TimeSpan > timeLastSync)
            {
                listOfLastMessages.Add(lastMessage.Value);
                lastMessage = lastMessage.Previous;
            }

            return listOfLastMessages;
        }

        public ResponseCode SendMessage(string sender, string reciever, string text)
        {
            if (!_contactsMapping.ValidateContact(sender, reciever))
            {
                return ResponseCode.RecieverNotFound;
            }

            if (!_userMessages.ContainsKey(reciever))
            {
                _userMessages.Add(reciever, new LinkedList<Message>());
            }

            if (!_userMessages.ContainsKey(sender))
            {
                _userMessages.Add(sender, new LinkedList<Message>());
            }

            Message k = new Message();
            k.TimeSpan = DateTime.Now;
            k.Sender = sender;
            k.Reciever = reciever;
            k.Text = text;
            _userMessages[reciever].AddLast(k);
            _userMessages[sender].AddLast(k);

            return ResponseCode.Success;
        }
    }
}
