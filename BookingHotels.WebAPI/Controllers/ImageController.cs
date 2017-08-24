using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingHotels.WebAPI.Models;
using System.Diagnostics;

namespace OwinSelfhostSample
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        public string imagesRootPath = Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\"));

        // GET api/image
        public HttpResponseMessage Get(string Id)
        {
            // Get path (console application)
            string imagesPath = Path.GetFullPath(Path.Combine(imagesRootPath, Id));

            // Set content as string array:
            string[] filePaths = {
            imagesPath + @"\room.jpg",
            imagesPath + @"\room1.jpg",
            };

            //string filePaths2 = JsonConvert.SerializeObject(filePaths, Formatting.Indented);
            HttpResponseMessage result = Request.CreateResponse<string[]>(HttpStatusCode.OK, filePaths);

            //// Send content as ByteArrayContent (slower than string array of paths):
            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Image image = Image.FromStream(fileStream);
            //MemoryStream memoryStream = new MemoryStream();
            //image.Save(memoryStream, ImageFormat.Jpeg);
            //result.Content = new ByteArrayContent(memoryStream.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }
        
    // Post
    [HttpPost]
    [Route("api/Image/Upload")]
    public HttpResponseMessage UploadPicture([FromBody] RoomImageUploadModel model)
    {
            // model.Image contains byte[]
            if (model.Image!=null)
            {
                // Generate id and name for image
                string imageName = Guid.NewGuid().ToString();
                // Create folder if not exists
                System.IO.Directory.CreateDirectory(imagesRootPath + model.RoomId);
                // Save image to filesystem
                var fs = new BinaryWriter(new FileStream(imagesRootPath + model.RoomId + '\\' + imageName + ".jpg", FileMode.Append, FileAccess.Write));
                fs.Write(model.Image);
                fs.Close();
                string message1 = "===\n Image uploaded, new image name: " + imageName;
                Debug.WriteLine(message1);
                return Request.CreateErrorResponse(HttpStatusCode.Created, imageName);
            }
            else
            {
                  return Request.CreateResponse(HttpStatusCode.BadRequest, model.ToString());
            }
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