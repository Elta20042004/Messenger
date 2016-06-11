using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.Test
{
    [TestClass]
    public class ContactsMappingTest
    {
        [TestMethod]
        public void AddContact_Add2Contacts_Success()
        {
            ContactsMapping contactsMapping = new ContactsMapping();
            contactsMapping.AddContact("Moshe", "Avi");
            contactsMapping.AddContact("Moshe", "Boris");
            contactsMapping.AddContact("Boris", "Moshe");
        }

        [TestMethod]
        public void ValidateContact_Add2Contacts_Success()
        {
            ContactsMapping contactsMapping = new ContactsMapping();
            contactsMapping.AddContact("Moshe", "Avi");
            contactsMapping.AddContact("Moshe", "Boris");
            contactsMapping.AddContact("Boris", "Moshe");

            Assert.IsFalse(contactsMapping.ValidateContact("Moshe", "Avi"));
            Assert.IsTrue(contactsMapping.ValidateContact("Avi", "Moshe"));
            Assert.IsFalse(contactsMapping.ValidateContact("Avi", "Boris"));
        }


    }
}
