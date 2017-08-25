using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingHotels.WebAPI.Models;
using System.Diagnostics;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BookingHotels.WebAPI
{

    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        public string imagesRootPath = Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\"));


        // GET api/image
        public HttpResponseMessage Get(string roomId, [FromUri] List<string> imageIds)
        {
            if (!imageIds.Contains(null))
            {
                //Debug.WriteLine( "recieved a list with length: " + imageIds.Length);
                Debug.WriteLine("requestedRoom: " + roomId);
                Debug.WriteLine("requestedImages: " + imageIds.ToString());
            

                // Get path (console application)
                string roomImagesPath = Path.GetFullPath(Path.Combine(imagesRootPath, roomId));
                // Set response content as string array of files paths:
                string[] filePaths = new string[imageIds.Count];
                int i = 0;
                foreach (string imageId in imageIds)
                {
                    filePaths[i] = roomImagesPath + @"\" + imageId + ".jpg";
                    i++;
                }
                HttpResponseMessage result = Request.CreateResponse<string[]>(HttpStatusCode.OK, filePaths);
                return result;
            }
            else
                return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // POST api/Image/Upload
        [HttpPost]
        [Route("api/Image/Upload")]
        public IHttpActionResult UploadPicture([FromBody] RoomImageUploadModel model)
        {
            // model.Image contains byte[]
            if (model.Image!=null)
            {
                // Generate Guid as new image name in UploadResult constructor
                ImageUploadResult imageUploadResult = new ImageUploadResult();
                // Create folder if not exists
                System.IO.Directory.CreateDirectory(imagesRootPath + model.RoomId);
                // Save image to filesystem
                var fs = new BinaryWriter(new FileStream(
                    imagesRootPath + model.RoomId + '\\' + imageUploadResult.Id.ToString() + ".jpg",
                    FileMode.Append, FileAccess.Write));
                fs.Write(model.Image);
                fs.Close();
                string message1 = "===\n Image uploaded, new image name: " + imageUploadResult.Id.ToString();
                Debug.WriteLine(message1);

                //return Request.CreateErrorResponse(HttpStatusCode.Created, imageName);
                MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
                //return Request.CreateResponse<ImageUploadResult>(HttpStatusCode.Created, imageUploadResult, bsonFormatter);
                return Ok(imageUploadResult);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, model.ToString());
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



//// Send content as ByteArrayContent (slower):
//FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
//Image image = Image.FromStream(fileStream);
//MemoryStream memoryStream = new MemoryStream();
//image.Save(memoryStream, ImageFormat.Jpeg);
//result.Content = new ByteArrayContent(memoryStream.ToArray());
//result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");