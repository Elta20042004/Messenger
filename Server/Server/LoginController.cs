using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Server.Entities;

namespace Server
{
    public class LoginController : ApiController
    {
        private readonly LoginPasswordVerification _loginPasswordVerification;

        public LoginController()
        {
            _loginPasswordVerification =
                ServiceLocator.Current.GetInstance <LoginPasswordVerification>();
        }

        // POST http://localhost:9000/api/login?login=Moshe&password=bla
        public Response<bool> Post(string login, string password)
        {
            bool ok = _loginPasswordVerification.Authorization(login, password);
            if (ok)
            {
                return new Response<bool>(true, ResponseCode.Success);
            }
            return new Response<bool>(false, ResponseCode.InternalError);
        }
    }
}