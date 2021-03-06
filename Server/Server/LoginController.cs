﻿using System;
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
            Console.WriteLine(this.Request.RequestUri);
            bool ok = _loginPasswordVerification.Authorization(login, password);
            return new Response<bool>(ok, 
                ok 
                ? ResponseCode.Success
                : ResponseCode.InternalError);
        }
    }
}