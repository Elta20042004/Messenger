using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageStore
    {
        private Dictionary<string, LinkedList<Message>> _userMessages;

        public MessageStore()
        {
            _userMessages = new Dictionary<string, LinkedList<Message>>();
        }

        public List<Message> GetMessage(string myUserId, DateTime timeLastSync)
        {
            List<Message> listOfLastMessages = new List<Message>();

            if (_userMessages[myUserId] == null)
            {
                return listOfLastMessages;
            }

            LinkedListNode<Message> lastMessage = _userMessages[myUserId].Last;

            while (lastMessage.Value.TimeSpan > timeLastSync
                && lastMessage.Previous != null)
            {
                listOfLastMessages.Add(lastMessage.Value);
                lastMessage = lastMessage.Previous;
            }

            return listOfLastMessages;
        }


    }
}
