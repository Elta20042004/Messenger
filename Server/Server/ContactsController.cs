﻿using System;
using System.Collections.Generic;
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
        public Response<bool> Post(string userId, string newContactId)
        {
            Console.WriteLine(this.Request.RequestUri);
            _contactsMapping.AddContact(userId, newContactId);     
            return new Response<bool>(true, ResponseCode.Success);     
        }

        // DELETE http://localhost:9000/api/contacts?userId=Moshe&contactId=Avi
        public Response<bool> Delete(string userId, string contactId)
        {
            Console.WriteLine(this.Request.RequestUri);
            _contactsMapping.DeleteContact(userId, contactId);
            return new Response<bool>(true, ResponseCode.Success);
        }

        //GET http://localhost:9000/api/contacts?userId=Moshe
        public Response<IEnumerable<string>> Get(string userId)
        {
            Console.WriteLine(this.Request.RequestUri);
            IEnumerable<string> contacts = _contactsMapping.GetContacts(userId);
            return new Response<IEnumerable<string>>(contacts, ResponseCode.Success);
        } 
    }
}
