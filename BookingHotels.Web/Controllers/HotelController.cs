using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using BookingHotels.Web.Models;
using BookingHotels.BLL.Interfaces;
using AutoMapper;
using BookingHotels.BLL.DTO;

namespace BookingHotels.Web.Controllers
{
    public class HotelController : Controller
    {
        IHotelService hotelService;
        public HotelController(IHotelService serv)
        {
            hotelService = serv;
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
        public ActionResult Details(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotelDto = hotelService.GetHotelById(Id);
            var hotel = Mapper.Map<HotelDTO, HotelViewModel>(hotelDto);

            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotel/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
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