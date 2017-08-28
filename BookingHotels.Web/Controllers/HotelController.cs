using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using BookingHotels.Web.Models;
using BookingHotels.BLL.Interfaces;
using AutoMapper;
using BookingHotels.BLL.DTO;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace BookingHotels.Web.Controllers
{
    public class HotelController : Controller
    {
        IHotelService hotelService;
        IFeedbackService feedbackService;
        public HotelController(IHotelService hotelServ, IFeedbackService feedbackServ)
        {
            hotelService = hotelServ;
            feedbackService = feedbackServ;
        }

        // GET: Hotel/Index
        public ActionResult Index()
        {
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            // Map DTO to ViewModel using Dtos data
            var hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            return View(hotels);
        }

        // GET: Hotel/Details/{Guid}
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotelDto = hotelService.GetHotelById(id);
            var hotel = Mapper.Map<HotelDTO, HotelViewModel>(hotelDto);

            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            var feedbackDtos = feedbackService.GetFeedbacksByHotelId(id);
            var feedbackViewModels = Mapper.Map<IEnumerable<FeedbackDTO>, IEnumerable<FeedbackViewModel>>(feedbackDtos);
            ViewBag.feedbacks = feedbackViewModels;

            return View(hotel);
        }

        // GET: Hotel/Feedback
        [Authorize]
        public ActionResult Feedback(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            // Populate hotel details
            var hotel = hotelService.GetHotelById(id);
            ViewBag.HotelId = id.ToString();
            ViewBag.HotelName = hotel.HotelName;
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel
            {
                HotelId = id,
                ApplicationUserId = userId
            };
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            return View(feedbackViewModel);
        }
        // POST: Hotel/Feedback
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                FeedbackDTO feedbackDto = Mapper.Map<FeedbackViewModel, FeedbackDTO>(feedbackViewModel);
                feedbackDto.Id = Guid.NewGuid();
                feedbackService.AddFeedback(feedbackDto);

                TempData["ErrorMessage"] = "Thanks for you feedback!";
                return RedirectToAction("Details",new { id = feedbackViewModel.HotelId.ToString() });
            }
            // Repopulate hotel details
            var hotel = hotelService.GetHotelById(feedbackViewModel.HotelId);
            ViewBag.HotelId = hotel.Id.ToString();
            ViewBag.HotelName = hotel.HotelName;
            TempData["ErrorMessage"] = "Check all fields and try again";
            return View();
        }
        
        /****
         * Admin Actions:
         ***/
         
        // GET: Hotel/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelViewModel hotelViewModel)
        {
            if (ModelState.IsValid)
            {
                HotelDTO hotelDto = Mapper.Map<HotelViewModel, HotelDTO>(hotelViewModel);
                hotelDto.Id = Guid.NewGuid();
                hotelService.AddHotel(hotelDto);
                return RedirectToAction("Index");
            }
            return View(hotelViewModel);
        }

        // GET: Hotel/Delete/{Guid}
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotelDto = hotelService.GetHotelById(Id);
            var hotelViewModel = Mapper.Map<HotelDTO, HotelViewModel>(hotelDto);
            if (hotelViewModel == null)
            {
                return HttpNotFound();
            }
            return View(hotelViewModel);
        }

        // POST: Hotel/Delete/{Guid}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid Id)
        {
            HotelDTO hotelDto = hotelService.GetHotelById(Id);
            hotelService.DeleteHotel(hotelDto);
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                hotelService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}