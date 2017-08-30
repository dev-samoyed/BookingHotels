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

        // Get Images folder path
        public string imagesRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/");
        // GET api/image
        public HttpResponseMessage Get(string roomId, [FromUri] List<string> imageIds)
        {
            if (!imageIds.Contains(null))
            {
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
            // Check if model.Image contains byte[]
            if (model.Images!=null)
            {
                // Generate Guid as Id and new image name in UploadResult constructor
                ImageUploadResult imageUploadResult = new ImageUploadResult(model.Images.Count);
                // Create folder if not exists
                System.IO.Directory.CreateDirectory(imagesRootPath + model.RoomId);
                int i = 0;
                // Save images to filesystem
                foreach (byte[] bytes in model.Images) {
                    var fs = new BinaryWriter(new FileStream(
                        imagesRootPath + model.RoomId + '\\' + imageUploadResult.Id[i].ToString() + ".jpg",
                        FileMode.Append, FileAccess.Write));
                    fs.Write(bytes);
                    fs.Close();
                    i++;
                }
                MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
                imageUploadResult.HttpStatusCode = HttpStatusCode.Created;
                return Ok(imageUploadResult);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, model.ToString());
            }
        }

        //// PUT api/image/5 
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/image/5 
        //public void Delete(int id)
        //{
        //}
    }
}