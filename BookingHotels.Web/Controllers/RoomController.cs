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

namespace BookingHotels.Web.Controllers
{
    public class RoomController : Controller
    {
        IRoomService roomService;
        IHotelService hotelService;
        IBookingService bookingService;
        IUserService userService;
        public RoomController(IRoomService serv, IHotelService hotelServ, IUserService userServ, IBookingService bookingServ)
        {
            roomService = serv;
            hotelService = hotelServ;
            bookingService = bookingServ;
            userService = userServ;
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
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDTO roomDto = roomService.GetRoom(id);
            RoomViewModel room = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);

            ViewBag.hotelName = hotelService.GetHotel(room.HotelId).HotelName;

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        // Get images src
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
        
        // Room/Edit
        public ActionResult Edit(Guid id)
        {           
            string baseAddress = "http://localhost:9000/";
            // Create HttpClient and make a request to api/image 
            HttpClient client = new HttpClient();
            // Response
            var response = client.GetAsync(baseAddress + "api/image/").Result;
            ViewBag.response = response;
            // Response Content
            string[] paths = response.Content.ReadAsAsync<string[]>().Result;
            ViewBag.responseContent = paths;
            // Get images Srcs
            ViewBag.imgSrcs = GetImageSrc(paths);
            //// Create selectlist of rooms
            //var roomDtos = roomService.GetRooms();
            //List<RoomViewModel> rooms = Mapper.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(roomDtos);
            //// Send SelectList of rooms to link images to them
            //ViewBag.rooms = new SelectList(rooms, "Id", "Id");
            // Get edited room
            var roomDto = roomService.GetRoom(id);
            RoomViewModel room = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);
            ViewBag.room = room;
            
            return View();
        }
        
        // GET: Room/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            // Map DTO to ViewModel using Dtos data
            List<HotelViewModel> hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            // Send SelectList of hotels to link rooms to them
            ViewBag.hotels = new SelectList(hotels, "Id", "HotelName");
            return View();
        }

        // POST
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
            // Repopulating
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            // Map DTO to ViewModel using Dtos data
            List<HotelViewModel> hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            // Sent hotels SelectList to viewBag
            ViewBag.hotels = new SelectList(hotels, "Id", "HotelName");
            return View(roomViewModel);
        }

        // GET: Room/Delete/{Guid}
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDTO roomDto = roomService.GetRoom(id);
            var roomViewModel = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);
            if (roomViewModel == null)
            {
                return HttpNotFound();
            }
            return View(roomViewModel);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            RoomDTO roomDto = roomService.GetRoom(id);
            roomService.DeleteRoom(roomDto);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        // GET: Room/Book/{Guid}
        public ActionResult Book(Guid id)
        {
            // Get room which we want to book
            RoomDTO roomDto = roomService.GetRoom(id);
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

        // POST        
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
            RoomDTO roomDto = roomService.GetRoom(bookingViewModel.RoomId);
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
        //// gists
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
