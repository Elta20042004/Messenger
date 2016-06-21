using System.Collections.Generic;
using System.Threading;

namespace Server
{
    public class ContactsMapping : IContactsMapping
    {
        private readonly Dictionary<string, HashSet<string>> _userContacts;
        private readonly ReaderWriterLockSlim _userContactsLock;

        public ContactsMapping()
        {
            _userContactsLock = new ReaderWriterLockSlim();
            _userContacts = new Dictionary<string, HashSet<string>>();
            _userContacts.Add("Lena",new HashSet<string>());
        }

        public bool ValidateContact(string sender, string reciever)
        {
            _userContactsLock.EnterReadLock();
            try
            {
                if (_userContacts.ContainsKey(reciever))
                {
                    if (_userContacts[reciever].Contains(sender))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                _userContactsLock.ExitReadLock();
            }

            return false;
        }

        public IEnumerable<string> GetContacts(string userId)
        {
            _userContactsLock.EnterReadLock();
            try
            {
                return _userContacts[userId];
            }
            finally
            {
                _userContactsLock.ExitReadLock();
            }
        }

        public void AddContact(string userId, string newContact)
        {
            _userContactsLock.EnterWriteLock();
            try
            {
                if (!_userContacts.ContainsKey(userId))
                {
                    _userContacts.Add(userId, new HashSet<string>());
                }

                _userContacts[userId].Add(newContact);
            }
            finally
            {
                _userContactsLock.ExitWriteLock();
            }
        }

        public void DeleteContact(string userId, string contact)
        {
            _userContactsLock.EnterWriteLock();
            try
            {
                _userContacts[userId].Remove(contact);
            }
            finally
            {
                _userContactsLock.ExitWriteLock();
            }
        }
    }
}