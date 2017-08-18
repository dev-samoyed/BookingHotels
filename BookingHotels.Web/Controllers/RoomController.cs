using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookingHotels.Web.Models;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.DTO;
using AutoMapper;
using System.Net;
using Microsoft.AspNet.Identity;

namespace BookingHotels.Web.Controllers
{
    public class RoomList
    {
        public List<RoomViewModel> Rooms { get; set; }
        public RoomList()
        {
            Rooms = new List<RoomViewModel>();
        }
    }

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

        // GET: Room/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            // Map DTO to ViewModel using Dtos data
            List<HotelViewModel> hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
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
