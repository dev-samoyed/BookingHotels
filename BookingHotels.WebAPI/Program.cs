using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace OwinSelfhostSample
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                //// Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //// Some room 
                //string room = "1D759E1A-B865-4B57-8845-2C7A4CB08E80";
                //var response = client.GetAsync(baseAddress + "api/image/"+room).Result;
                //Console.WriteLine("Response: ");
                //Console.WriteLine(response);
                //Console.WriteLine("Response content: ");
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}