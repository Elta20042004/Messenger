using System.Collections.Generic;

namespace Server
{
    public interface IContactsMapping
    {
        void AddContact(string userId, string newContact);

        void DeleteContact(string userId, string contact);

        IEnumerable<string> GetContacts(string userId);

        void CreateNewUser(string userId);
    }
}