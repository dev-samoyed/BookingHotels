using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookingHotels.Web.Models;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.DTO;
using AutoMapper;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace BookingHotels.Web.Controllers
{
    public class RoomController : Controller
    {
        IRoomService roomService;
        IHotelService hotelService;
        IBookingService bookingService;
        IUserService userService;
        IRoomImageService roomImageService;
        public RoomController(IRoomService serv, IHotelService hotelServ, IUserService userServ, IBookingService bookingServ, IRoomImageService roomImageServ)
        {
            roomService = serv;
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
                    BaseAddress = new Uri("http://localhost:9000/")
                };
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }
        }

        // Room/Index
        public ActionResult Index()
        {
            // Get all rooms
            IEnumerable<RoomDTO> roomDtos = roomService.GetRooms();
            IEnumerable<BookingDTO> bookings = bookingService.GetBookings();
            ViewBag.bookings = bookings;
            // Map DTO to ViewModel using Dtos data
            var rooms = Mapper.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(roomDtos);
            return View(rooms);
        }

        // Room/Details/{Guid}
        public ActionResult Details(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDTO roomDto = roomService.GetRoomById(Id);
            RoomViewModel room = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);

            ViewBag.hotelName = hotelService.GetHotelById(room.HotelId).HotelName;

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // Get images src method
        public string[] GetImageSrc(string[] filePaths)
        {
            //string[] result = new string[filePaths.Length];
            // Download images
            for (int i=0;i< filePaths.Length; i++)
            {
                byte[] imageByteData = System.IO.File.ReadAllBytes(filePaths[i]);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                filePaths[i] = imageDataURL;
            }
            return filePaths;
        }

        //List<RootObject> datalist = JsonConvert.DeserializeObject<List<RootObject>>(jsonstring);
        //to something like this :

        //RootObject datalist = JsonConvert.DeserializeObject<RootObject>(jsonstring);


        //GET Room/Edit
        public ActionResult Edit(Guid Id)
        {    
            // Get images Ids for this room
            List<RoomImageDTO> roomImageDTOs = roomImageService.GetRoomImagesByRoomId(Id).ToList();

            List<string> imageIdsList = new List<string>();
            foreach (var roomImage in roomImageDTOs)
            {
                imageIdsList.Add(roomImage.Id.ToString());
            }
            // Image Ids that will be send as url parameters
            string imageIDs="";
            foreach (var imageId in imageIdsList)
                imageIDs += "imageIDs="+imageId + "&";
            string url = string.Format(Client.BaseAddress + "api/image/?roomId={0}&{1}", Id, imageIDs);
            // Get response from request to api/image
            var response = Client.GetAsync(url).Result;
            if ((int)response.StatusCode==200) {
                string[] paths = response.Content.ReadAsAsync<string[]>().Result;
                // Get images Srcs for this room
                ViewBag.imgSrcs = GetImageSrc(paths);
            }
            
            //check ErrorMessage value
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;

            // Get edited room
            var roomDto = roomService.GetRoomById(Id);
            RoomViewModel roomViewModel = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);
            return View(roomViewModel);
        }

        // POST: Upload room image to api/Image/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadRoomImage(RoomViewModel roomViewModel, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    var roomImageUploadModel = new RoomImageUploadModel
                    {
                        RoomId = roomViewModel.Id
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
                    var response = await Client.PostAsync<RoomImageUploadModel>("api/Image/Upload/", roomImageUploadModel, bsonFormatter);
                    Debug.WriteLine("Server responsed: " + response);
                    // If response is Ok
                    if ((int)response.StatusCode == 200)
                    {
                        // Use BSON formatter to deserialize the response content
                        MediaTypeFormatter[] formatters = new MediaTypeFormatter[] {
                            new BsonMediaTypeFormatter()
                        };
                        ImageUploadResult imageUploadResult = await response.Content.ReadAsAsync<ImageUploadResult>(formatters);
                        Debug.WriteLine("Send to db: " + imageUploadResult.Id);
                        // Get image name generated on server
                        roomImageUploadModel.Id = imageUploadResult.Id;
                        // Send to Database
                        RoomImageDTO roomImageDTO = Mapper.Map<RoomImageUploadModel, RoomImageDTO>(roomImageUploadModel);
                        roomImageService.Create(roomImageDTO);
                        TempData["ErrorMessage"] = "Image uploaded and creaded a record in Database";
                        return RedirectToAction("Edit", new { id = roomViewModel.Id.ToString() });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Bad responce StatusCode "+ (int)response.StatusCode;
                        return RedirectToAction("Edit", new { id = roomViewModel.Id.ToString() });
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "File is empty";
                    return RedirectToAction("Edit", new { id = roomViewModel.Id.ToString() });
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Room model state is not valid";
                return RedirectToAction("Edit", new { id = roomViewModel.Id.ToString() });
            }
        }

        // GET: Room/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            // Map DTO to ViewModel using Dtos data
            List<HotelViewModel> hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            // Create SelectList of hotels to link rooms to them
            ViewBag.hotels = new SelectList(hotels, "Id", "HotelName");
            return View();
        }

        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomViewModel roomViewModel)
        {
            if (ModelState.IsValid)
            {
                RoomDTO roomDto = Mapper.Map<RoomViewModel, RoomDTO>(roomViewModel);
                roomDto.Id = Guid.NewGuid();
                roomService.AddRoom(roomDto);
                return RedirectToAction("Index");
            }
            // Repopulating hotels SelectList
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            List<HotelViewModel> hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            ViewBag.hotels = new SelectList(hotels, "Id", "HotelName");
            return View(roomViewModel);
        }

        // GET: Room/Delete/{Guid}
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDTO roomDto = roomService.GetRoomById(Id);
            var roomViewModel = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);
            if (roomViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roomViewModel);
        }

        // POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid Id)
        {
            RoomDTO roomDto = roomService.GetRoomById(Id);
            roomService.DeleteRoom(roomDto);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        // GET: Room/Book/{Guid}
        public ActionResult Book(Guid id)
        {
            // Get room which we want to book
            RoomDTO roomDto = roomService.GetRoomById(id);
            ViewBag.RoomType = roomDto.RoomType.ToString();
            ViewBag.Price = roomDto.RoomPrice.ToString();
            ViewBag.Hotel = roomDto.Hotel.HotelName.ToString();
            ViewBag.HotelStars = roomDto.Hotel.HotelStars.ToString();
            // Create bookingViewModel
            BookingViewModel bookingViewModel = new BookingViewModel();
            bookingViewModel.RoomId = id;
            bookingViewModel.ApplicationUserId = Guid.Parse(User.Identity.GetUserId());
            return View(bookingViewModel);
        }

        // POST Book   
        [HttpPost, ActionName("Book")]
        public ActionResult BookConfirmed(BookingViewModel bookingViewModel)
        {
            if (ModelState.IsValid)
            {
                var startDateDesired = bookingViewModel.BookingStartDate;
                var endDateDesired = bookingViewModel.BookingEndDate;
                if (startDateDesired <= endDateDesired) 
                {
                    List<object> result = bookingService.IsRoomOccupied(bookingViewModel.RoomId, startDateDesired, endDateDesired);
                    // Is occupied?
                    if ((bool)result[0])
                    {
                        return Content("Sorry, the room is occupied from " + result[1] + " to "+ result[2] + "<a href='javascript: history.back()'>Go Back</a>");
                    }
                    else
                    {
                        BookingDTO bookingDto = Mapper.Map<BookingViewModel, BookingDTO>(bookingViewModel);
                        // Generate Id for new booking
                        bookingDto.Id = Guid.NewGuid();
                        bookingService.CreateBooking(bookingDto);
                        return Content("<h2>You have succesfully booked this room</h2><a href='javascript: history.back()'>Go Back</a>");
                    }
                }
                return Content("<h2>Start date must be less than end date</h2><a href='javascript: history.back()'>Go Back</a>");
            }
            // Repopulate room details
            RoomDTO roomDto = roomService.GetRoomById(bookingViewModel.RoomId);
            ViewBag.RoomType = roomDto.RoomType.ToString();
            ViewBag.Price = roomDto.RoomPrice.ToString();
            ViewBag.Hotel = roomDto.Hotel.HotelName.ToString();
            ViewBag.HotelStars = roomDto.Hotel.HotelStars.ToString();
            return View();
        }

        // Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                roomService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
        //// gists :)
        //public Image GetImageSrc4(string filePath)
        //{
        //    // WebClient wc = new WebClient();
        //    // byte[] bytes = wc.DownloadData(path);
        //    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        //    // Read image
        //    using (var ms = new MemoryStream(bytes))
        //    {
        //        return Image.FromStream(ms);
        //    }
        //}

        //public Image GetImageSrc3(string filePath)
        //{
        //    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    Image image = Image.FromStream(fileStream);
        //    MemoryStream memoryStream = new MemoryStream();
        //    image.Save(memoryStream, ImageFormat.Jpeg);
        //    return image;
        //}
        
        //public ActionResult GetImageSrc2(string filePath)
        //{
        //    var watch2 = Stopwatch.StartNew();
        //    byte[] imageByteData = System.IO.File.ReadAllBytes(filePath);
        //    watch2.Stop();
        //    Debug.WriteLine("GetImageSrc2: " + watch2.ElapsedMilliseconds);
        //    return File(imageByteData, "image/jpeg");
        //}
