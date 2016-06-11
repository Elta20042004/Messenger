namespace Server
{
    public interface IContactsMapping
    {
        bool ValidateContact(string sender, string reciever);
        void AddContact(string userId, string newContact);
        void DeleteContact(string userId, string contact);
    }
}