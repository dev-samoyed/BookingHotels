using Microsoft.Owin.Hosting;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Web.Hosting;

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
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                //var path = HostingEnvironment.MapPath("~/Images/room.jpg"); //null
                AppDomain root = AppDomain.CurrentDomain;
                AppDomainSetup setup = new AppDomainSetup();
                setup.ApplicationBase = root.SetupInformation.ApplicationBase + @"..\..\Images\";
                AppDomain domain = AppDomain.CreateDomain("ImagesDomain", null, setup);
                string path = domain.SetupInformation.ApplicationBase;
                Console.WriteLine("path = " + path);
                AppDomain.Unload(domain);

                
                //var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                //var path = new Uri(uriPath).LocalPath;

                var response = client.GetAsync(baseAddress + "api/image").Result;

                
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }
        }
    }
}