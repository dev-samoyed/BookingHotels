using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingHotels.WebAPI.Models;
using System.Diagnostics;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace BookingHotels.WebAPI
{
    // Object to sent as post result
    public class ImageUploadResult
    {
        public Guid Id;
        public HttpStatusCode HttpStatusCode;
        public ImageUploadResult()
        {
            Id = Guid.NewGuid();
        }
    }
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