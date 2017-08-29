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
using System.Threading.Tasks;
using System.Linq;

namespace BookingHotels.Web.Controllers
{
    public class RoomController : BaseController
    {
        //IRoomService roomService;
        //IHotelService hotelService;
        //IBookingService bookingService;
        //IUserService userService;
        //IRoomImageService roomImageService;
        //public RoomController(IRoomService serv, IHotelService hotelServ, IUserService userServ, IBookingService bookingServ, IRoomImageService roomImageServ)
        //{
        //    roomService = serv;
        //    hotelService = hotelServ;
        //    bookingService = bookingServ;
        //    userService = userServ;
        //    roomImageService = roomImageServ;
        //}

            public RoomController(
            IRoomService roomServ, 
            IHotelService hotelServ, 
            IUserService userServ,
            IBookingService bookingServ,
            IRoomImageService roomImageServ) 
            : base(roomServ, hotelServ, userServ, bookingServ, roomImageServ)
            { }

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
            // Get images paths by room id from Web Api
            string[] paths = GetImagesPathsByRoomId(Id);
            // Get images Srcs for this room and send to view
            ViewBag.imgSrcs = GetImageSrcs(paths);

            RoomDTO roomDto = roomService.GetRoomById(Id);
            RoomViewModel room = Mapper.Map<RoomDTO, RoomViewModel>(roomDto);

            ViewBag.hotelName = hotelService.GetHotelById(room.HotelId).HotelName;

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
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
            // Check ErrorMessage value
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
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
                        DateTime date1 = DateTime.Parse(result[1].ToString());
                        DateTime date2 = DateTime.Parse(result[2].ToString());
                        //ModelState.AddModelError("BookingEndDate", "qqq");
                        TempData["ErrorMessage"] = "Sorry, the room is occupied from " + date1.ToShortDateString() + " to " + date1.ToShortDateString();
                        return RedirectToAction("Book", new { id = bookingViewModel.RoomId.ToString() });
                    }
                    else
                    {
                        BookingDTO bookingDto = Mapper.Map<BookingViewModel, BookingDTO>(bookingViewModel);
                        // Generate Id for new booking
                        bookingDto.Id = Guid.NewGuid();
                        // Save to database
                        bookingService.CreateBooking(bookingDto);
                        TempData["ErrorMessage"] = "You have succesfully booked this room from " + startDateDesired.ToShortDateString() + " to " + endDateDesired.ToShortDateString();
                        return RedirectToAction("Book", new { id = bookingViewModel.RoomId.ToString() });
                    }
                }
                TempData["ErrorMessage"] = "Start date must be less than end date";
                return RedirectToAction("Book", new { id = bookingViewModel.RoomId.ToString() });
            }
            // Repopulate room details
            RoomDTO roomDto = roomService.GetRoomById(bookingViewModel.RoomId);
            ViewBag.RoomType = roomDto.RoomType.ToString();
            ViewBag.Price = roomDto.RoomPrice.ToString();
            ViewBag.Hotel = roomDto.Hotel.HotelName.ToString();
            ViewBag.HotelStars = roomDto.Hotel.HotelStars.ToString();
            return View();
        }

        /****
         * Admin Actions:
         ***/

        // GET Room/Edit
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Guid Id)
        {
            // Get images paths by room id from Web Api
            string[] paths = GetImagesPathsByRoomId(Id);
            // Get images Srcs for this room and send to view
            ViewBag.imgSrcs = GetImageSrcs(paths);
            // Check ErrorMessage value
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
                    var roomImageUploadModel = new RoomImageUploadModel {RoomId = roomViewModel.Id};
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
                    // If response is Ok
                    if ((int)response.StatusCode == 200)
                    {
                        // Use BSON formatter to deserialize the response content
                        MediaTypeFormatter[] formatters = new MediaTypeFormatter[] {
                            new BsonMediaTypeFormatter()
                        };
                        ImageUploadResult imageUploadResult = await response.Content.ReadAsAsync<ImageUploadResult>(formatters);
                        // Get image name generated on server
                        roomImageUploadModel.Id = imageUploadResult.Id;
                        // Send to Database
                        RoomImageDTO roomImageDTO = Mapper.Map<RoomImageUploadModel, RoomImageDTO>(roomImageUploadModel);
                        roomImageService.Create(roomImageDTO);
                        TempData["ErrorMessage"] = "Image uploaded and a record has been creaded in database";
                        return RedirectToAction("Edit", new { id = roomViewModel.Id.ToString() });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Bad responce, Status Code " + (int)response.StatusCode;
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
    }
}