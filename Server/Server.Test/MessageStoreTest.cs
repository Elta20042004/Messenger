using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Entities;

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

            Assert.AreEqual(ResponseCode.RecieverNotFound, messageStore.SendMessage("Moshe", "Avi", "hello").ErrorCode);
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "Hello").ErrorCode);
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "How are you").ErrorCode);
        }

        [TestMethod]
        public void GetMessage_thisUserAndData()
        {
            ContactsMapping contactsMapping = new ContactsMapping();
            contactsMapping.AddContact("Moshe", "Avi");
            contactsMapping.AddContact("Moshe", "Boris");
            contactsMapping.AddContact("Boris", "Moshe");

            MessageStore messageStore = new MessageStore(contactsMapping);

            Assert.AreEqual(ResponseCode.RecieverNotFound, messageStore.SendMessage("Moshe", "Avi", "hello").ErrorCode);
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "Hello").ErrorCode);
            Assert.AreEqual(ResponseCode.Success, messageStore.SendMessage("Moshe", "Boris", "How are you").ErrorCode);

            var rez = messageStore.GetMessage("Boris", DateTime.Now.AddSeconds(-5));
            Assert.AreEqual("Moshe", rez.Data[0].Sender);
            Assert.AreEqual("Boris", rez.Data[0].Reciever);
            Assert.AreEqual("Moshe", rez.Data[1].Sender);
            Assert.AreEqual("Boris", rez.Data[1].Reciever);
            Assert.AreEqual(2, rez.Data.Count);

            var rez2 = messageStore.GetMessage("Moshe", DateTime.Now.AddSeconds(-5));
            Assert.AreEqual(2, rez2.Data.Count);
            Assert.AreEqual("Moshe", rez2.Data[0].Sender);
            Assert.AreEqual("Boris", rez2.Data[0].Reciever);
            Assert.AreEqual("Moshe", rez2.Data[1].Sender);
            Assert.AreEqual("Boris", rez2.Data[1].Reciever);
        }
    }
}
