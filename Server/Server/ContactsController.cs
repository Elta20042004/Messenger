using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Server.Entities;

namespace Server
{
    public class ContactsController : ApiController
    {
        private readonly IContactsMapping _contactsMapping;
        public ContactsController()
        {
            _contactsMapping =
                ServiceLocator.Current.GetInstance<IContactsMapping>();
        }

        // POST http://localhost:9000/api/contacts?userId=Moshe&newContactId=Avi
        public ResponseCode Post(string userId, string newContactId)
        {
            _contactsMapping.AddContact(userId, newContactId);     
            return  ResponseCode.Success;     
        }

        // DELETE http://localhost:9000/api/contacts?userId=Moshe&contactId=Avi
        public ResponseCode Delete(string userId, string contactId)
        {
            _contactsMapping.DeleteContact(userId, contactId);
            return ResponseCode.Success;
        }

        //GET http://localhost:9000/api/contacts?userId=Moshe
        public IEnumerable<string> Get(string userId)
        {
            IEnumerable<string> contacts = _contactsMapping.GetContacts(userId);
            return contacts;
        } 
    }
}
