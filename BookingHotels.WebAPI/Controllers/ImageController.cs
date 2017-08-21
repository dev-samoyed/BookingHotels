using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;
using Microsoft.Owin.Hosting;
// using System.Web.UI.WebControls;
using System.Net.Http;
using System.Web.Http;
using System.Web.Hosting;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
//using static System.Net.Mime.MediaTypeNames;

namespace OwinSelfhostSample
{
    public class ImageController : ApiController
    {


        //// GET api/values 
        //public IEnumerable<string> Get()
        //{

        //    return new string[] { "value1", "value2" };
        //}

        public HttpResponseMessage Get()
        {

            AppDomain root = AppDomain.CurrentDomain;
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = root.SetupInformation.ApplicationBase + @"..\..\Images\";
            AppDomain domain = AppDomain.CreateDomain("ImagesDomain", null, setup);
            string path = domain.SetupInformation.ApplicationBase;
            Console.WriteLine("path = " + path);
            AppDomain.Unload(domain);
            
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string filePath = path+"/room.jpg";
            //FileStream fileStream = new FileStream(filePath, FileMode.Open);
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            

            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }

        //// GET api/values/5 
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}