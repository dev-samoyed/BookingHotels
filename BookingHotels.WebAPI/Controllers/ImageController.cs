using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;
using Microsoft.Owin.Hosting;
// using System.Web.UI.WebControls;
using System.Net.Http;
using AutoMapper;
using System.Web.Http;
using BookingHotels.WebAPI.Models;
using System.Diagnostics;
using System.Reflection;
using System.Web.Hosting;
using System.Web;
using Newtonsoft.Json;
//using static System.Net.Mime.MediaTypeNames;

namespace OwinSelfhostSample
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        // GET api/image 
        public HttpResponseMessage Get(string Id)
        {
            // Get path (console application)
            string imagesPath = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\Images\", Id));

            // Set content as string array:
            string[] filePaths = {
            imagesPath + @"\room.jpg",
            imagesPath + @"\room1.jpg",

            };
            string filePaths2 = JsonConvert.SerializeObject(filePaths, Formatting.Indented);

            HttpResponseMessage result = Request.CreateResponse<string>(HttpStatusCode.OK, filePaths2);

            //// Send content as ByteArrayContent (slower than paths array):
            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //Image image = Image.FromStream(fileStream);
            //MemoryStream memoryStream = new MemoryStream();
            //image.Save(memoryStream, ImageFormat.Jpeg);
            //result.Content = new ByteArrayContent(memoryStream.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }
        
        //private readonly IGrowthFacade _facade;
        //#region Constructor
        //public ImageController()
        //{
        //    _facade = new GrowthFacade();
        //}
        //public ImageController(IGrowthFacade facade)
        //{
        //    _facade = facade as GrowthFacade;
        //}
        //#endregion Constructor        

      [HttpPost]
      [Route("api/Image/Upload")]
      public HttpResponseMessage UploadPicture([FromBody] RoomImageUploadModel model)
      {
          try
          {
              //var entity = Mapper.Map<SchoolFileUploadModel, SchoolImage>(model);
              //var response = _facade.CreateSchoolImage(entity);
              
              // If image uploaded
              if (true)
              {
                    // Generate name

                    string imageName = Guid.NewGuid().ToString();

                    // Save image to folder Images/{room.Id}
                    // .... 
                    
                    Debug.WriteLine("============\n Image uploaded, new image name = " + imageName);
                    return Request.CreateResponse(HttpStatusCode.Created, imageName);
              }
              else
              {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, model.ToString());
              }
          }
          catch (Exception ex)
          {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
          }
      }
      
        //[HttpPost]
        //[Route("post")]
        //[AllowAnonymous]
        //public async Task<HttpResponseMessage> Post()
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;

        //        foreach (string file in httpRequest.Files)
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

        //            var postedFile = httpRequest.Files[file];
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  
        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {
        //                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {
        //                    var message = string.Format("Please Upload a file upto 1 mb.");
        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else
        //                {
        //                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + postedFile.FileName + extension);
        //                    postedFile.SaveAs(filePath);
        //                }
        //            }
        //            var message1 = string.Format("Image Updated Successfully.");
        //            return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
        //        }
        //        var res = string.Format("Please Upload a image.");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = string.Format("some Message");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //}

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