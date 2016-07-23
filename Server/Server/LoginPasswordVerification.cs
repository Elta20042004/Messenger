using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace Server
{
    class LoginPasswordVerification
    {
        private readonly Dictionary<string, string> _loginPassword;
        private readonly IContactsMapping _contactsMapping;

        public LoginPasswordVerification()
        {
            _contactsMapping =
                ServiceLocator.Current.GetInstance<IContactsMapping>();
            _loginPassword = new Dictionary<string, string>();
        }

        public bool Authorization(string login, string password)
        {
            if (!_loginPassword.ContainsKey(login))
            {
                _loginPassword.Add(login, password);
                _contactsMapping.CreateNewUser(login);
                return true;
            }

            if (_loginPassword[login] == password)
            {
                return true;
            }

            return false;

        }
    }
}
