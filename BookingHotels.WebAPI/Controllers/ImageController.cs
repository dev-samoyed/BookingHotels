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
            // Get root path (method 1):
            //var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            //var path = new Uri(uriPath).LocalPath;

            // Get root path (method 2):
            AppDomain root = AppDomain.CurrentDomain;
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = root.SetupInformation.ApplicationBase + @"..\..\Images\";
            AppDomain domain = AppDomain.CreateDomain("ImagesDomain", null, setup);
            string imagesPath = domain.SetupInformation.ApplicationBase;
            AppDomain.Unload(domain);
            
            // Send content as strings array:
            string[] filePaths = { 
            imagesPath + "room.jpg",
             imagesPath + "room2.jpg",
              imagesPath + "room3.jpg",
              imagesPath + "room4.jpg",
              imagesPath + "room5.jpg",
              imagesPath + "room6.jpg"
            };
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, filePaths);

            //// Send content as ByteArrayContent (slower than paths array):
            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Image image = Image.FromStream(fileStream);
            //MemoryStream memoryStream = new MemoryStream();
            //image.Save(memoryStream, ImageFormat.Jpeg);
            //result.Content = new ByteArrayContent(memoryStream.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

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