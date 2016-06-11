using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;

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

        //Add Contact IEnumerable<string>
        // GET api/values 
        public void Get(string userId, string newContact)
        {
            _contactsMapping.AddContact(userId, newContact);            
        }

        // GET api/values/5 
        public void Delete(string userId, string contact)
        {
            _contactsMapping.DeleteContact(userId, contact);
        }      
    }
}
