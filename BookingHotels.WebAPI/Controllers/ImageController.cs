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
using System.Threading.Tasks;
using System.Web;
//using static System.Net.Mime.MediaTypeNames;

namespace OwinSelfhostSample
{
    [RoutePrefix("api/image")]
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

        [HttpPost]
        [Route("post")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  
                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");
                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName + extension);
                            postedFile.SaveAs(filePath);
                        }
                    }
                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        //// POST api/image 
        //public void Post([FromBody]string value)
        //{
        //}

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