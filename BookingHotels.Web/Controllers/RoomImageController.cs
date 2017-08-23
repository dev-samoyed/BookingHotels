using System;
using System.Web.Mvc;
using BookingHotels.Web.Models;
using System.Net.Http;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace BookingHotels.Web.Controllers
{
    public class RoomImageController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(RoomImageViewModel roomImageViewModel, HttpPostedFileBase uploadedFile)
        {   
            if (ModelState.IsValid)
            {
                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    var roomImageUploadModel = new RoomImageViewModel
                    {
                        //ImageName = Path.GetFileName(uploadedFile.FileName),
                        Id = Guid.NewGuid()
                        //RoomId = uploadedFile.FileName
                        //ContentType = uploadedFile.ContentType,
                        //ContentSize = uploadedFile.ContentLength
                    };
                    using (var reader = new BinaryReader(uploadedFile.InputStream))
                    {
                        roomImageUploadModel.Image = reader.ReadBytes(uploadedFile.ContentLength);
                    }
                    // Set the Accept header for BSON.
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
                    // POST using the BSON formatter.
                    MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
                    var response = Client.PostAsync<RoomImageViewModel>("Image/Upload", roomImageUploadModel, bsonFormatter);
                    response.Result.EnsureSuccessStatusCode();
                }
            }
            //TODO: set correct return
            return View();
        }
        public ActionResult Upload()
        {
            
            return View();
        }


        public HttpClient Client
        {
            get
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri("http://localhost:9000/api/")
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }
        }
    }
}