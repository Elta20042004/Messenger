using System.Collections.Generic;

namespace Server
{
    public class ContactsMapping : IContactsMapping
    {
        private readonly Dictionary<string, HashSet<string>> _userContacts;

        public ContactsMapping()
        {
            _userContacts = new Dictionary<string, HashSet<string>>();
        }

        public bool ValidateContact(string sender, string reciever)
        {
            if (_userContacts.ContainsKey(reciever))
            {
                if (_userContacts[reciever].Contains(sender))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddContact(string userId, string newContact)
        {
            if (!_userContacts.ContainsKey(userId))
            {
                _userContacts.Add(userId, new HashSet<string>());
            }

            _userContacts[userId].Add(newContact);
        }

        public void DeleteContact(string userId, string contact)
        {
            _userContacts[userId].Remove(contact);
        }
    }
}