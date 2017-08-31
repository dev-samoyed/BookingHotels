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
        public IFeedbackService feedbackService;
        // Room controller constructor
        public BaseController(IRoomService roomServ,
                            IHotelService hotelServ,
                            IUserService userServ,
                            IBookingService bookingServ,
                            IRoomImageService roomImageServ)
        {
            roomService = roomServ;
            hotelService = hotelServ;
            bookingService = bookingServ;
            userService = userServ;
            roomImageService = roomImageServ;
        }
        // Hotel controller constructor
        public BaseController(
                    IHotelService hotelServ,
                    IFeedbackService feedbackServ)
        {
            hotelService = hotelServ;
            feedbackService = feedbackServ;
        }
        // Account controller constructor
        public BaseController(
                    IUserService userServ)
        {
            userService = userServ;
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

        // Get images paths from Web Api by room Id
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
            try { 
                var response = Client.GetAsync(url).Result;
                if ((int)response.StatusCode == 200)
                {
                    return response.Content.ReadAsAsync<string[]>().Result;
                }
            }
            catch
            {
                return null;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    hotelService.Dispose();
                    roomService.Dispose();
                    bookingService.Dispose();
                    userService.Dispose();
                    roomImageService.Dispose();
                    feedbackService.Dispose();
                }
                catch { }
            }
            base.Dispose(disposing);
        }

    }
}