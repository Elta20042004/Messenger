using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.Net.Http;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // string baseAddress = "http://*:80/";
            string baseAddress = ConfigurationManager.AppSettings.Get("host");

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/message").Result;                
                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                //var response2 = client.GetAsync(baseAddress + "api/contacts").Result;
                //Console.WriteLine(response2);
                //Console.WriteLine(response2.Content.ReadAsStringAsync().Result);

                Console.ReadLine();
            }
        }
    }
}
