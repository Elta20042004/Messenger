using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.Test
{
    [TestClass]
    public class MessageStoreTest
    {
        [TestMethod]
        public void SendMessage_Add2ContactsAndSendText()
        {
            ContactsMapping contactsMapping = new ContactsMapping();
            contactsMapping.AddContact("Moshe", "Avi");
            contactsMapping.AddContact("Moshe", "Boris");
            contactsMapping.AddContact("Boris", "Moshe");

            MessageStore messageStore = new MessageStore(contactsMapping);

            Assert.AreEqual(ResponseCode.RecieverNotFound,messageStore.SendMessage("Moshe","Avi","hello"));
            Assert.AreEqual(ResponseCode.Success,messageStore.SendMessage("Moshe", "Boris","Hello"));
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "How are you"));
        }

        [TestMethod]
        public void GetMessage_thisUserAndData()
        {
            ContactsMapping contactsMapping = new ContactsMapping();
            contactsMapping.AddContact("Moshe", "Avi");
            contactsMapping.AddContact("Moshe", "Boris");
            contactsMapping.AddContact("Boris", "Moshe");

            MessageStore messageStore = new MessageStore(contactsMapping);

            Assert.AreEqual(ResponseCode.RecieverNotFound, messageStore.SendMessage("Moshe", "Avi", "hello"));
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "Hello"));
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "How are you"));

            var rez = messageStore.GetMessage("Boris", DateTime.Now.AddSeconds(-5));
            Assert.AreEqual(2,rez.Count);
        }
    }
}
