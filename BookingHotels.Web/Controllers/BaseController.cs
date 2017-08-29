using BookingHotels.BLL.DTO;
using BookingHotels.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BookingHotels.Web.Controllers
{
    public abstract class BaseController : Controller
    {

        public IRoomService roomService;
        public IHotelService hotelService;
        public IBookingService bookingService;
        public IUserService userService;
        public IRoomImageService roomImageService;
        public BaseController(IRoomService roomServ, IHotelService hotelServ, IUserService userServ, IBookingService bookingServ, IRoomImageService roomImageServ)
        {
            roomService = roomServ;
            hotelService = hotelServ;
            bookingService = bookingServ;
            userService = userServ;
            roomImageService = roomImageServ;
        }

        // Create HttpClient
        public HttpClient Client
        {
            get
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri("http://imgwebapi.com/")
                };
                return client;
            }
        }

        // Get Images paths from Web Api by Room Id
        public string[] GetImagesPathsByRoomId(Guid Id)
        {
            List<RoomImageDTO> roomImageDTOs = roomImageService.GetRoomImagesByRoomId(Id).ToList();
            // Get desired images Ids to send as url parameters
            List<string> imageIdsList = new List<string>();
            foreach (var roomImage in roomImageDTOs)
            {
                imageIdsList.Add(roomImage.Id.ToString());
            }
            string imageIDs = "";
            foreach (var imageId in imageIdsList)
                imageIDs += "imageIDs=" + imageId + "&";
            string url = string.Format(Client.BaseAddress + "api/image/?roomId={0}&{1}", Id, imageIDs);
            // Get response from request to api/image
            var response = Client.GetAsync(url).Result;
            if ((int)response.StatusCode == 200)
            {
                return response.Content.ReadAsAsync<string[]>().Result;
            }
            return null;
        }

        // Get images srcs
        public string[] GetImageSrcs(string[] filePaths)
        {
            // Download images
            if (filePaths != null)
                for (int i = 0; i < filePaths.Length; i++)
                {
                    byte[] imageByteData = System.IO.File.ReadAllBytes(filePaths[i]);
                    string imageBase64Data = Convert.ToBase64String(imageByteData);
                    string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                    filePaths[i] = imageDataURL;
                }
            return filePaths;
        }

    }
}