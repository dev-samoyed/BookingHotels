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
using System.Text;
//using static System.Net.Mime.MediaTypeNames;

namespace OwinSelfhostSample
{
    public class ImageController : ApiController
    {
        // GET api/image 
        public HttpResponseMessage Get()
        {
            //var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            //var path = new Uri(uriPath).LocalPath;

            AppDomain root = AppDomain.CurrentDomain;
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = root.SetupInformation.ApplicationBase + @"..\..\Images\";
            AppDomain domain = AppDomain.CreateDomain("ImagesDomain", null, setup);
            string imagesPath = domain.SetupInformation.ApplicationBase;
            AppDomain.Unload(domain);
            
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string filePath = imagesPath + "room.jpg";

            //// Send as ByteArrayContent:
            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Image image = Image.FromStream(fileStream);
            //MemoryStream memoryStream = new MemoryStream();
            //image.Save(memoryStream, ImageFormat.Jpeg);
            //result.Content = new ByteArrayContent(memoryStream.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            // result.Content = new StringContent(filePath, Encoding.UTF8, "text/html");
            result.Content = new StringContent(filePath);

            return result;
        }

        // POST api/image 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/image/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/image/5 
        public void Delete(int id)
        {
        }
    }
}