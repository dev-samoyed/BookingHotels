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
        
        // GET: HotelViewModels
        public ActionResult Index()
        {
            IEnumerable<HotelDTO> hotelDtos = hotelService.GetHotels();
            // Map DTO to ViewModel using Dtos data
            var hotels = Mapper.Map<IEnumerable<HotelDTO>, List<HotelViewModel>>(hotelDtos);
            return View(hotels);
        }

        // GET: HotelViewModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotelDto = hotelService.GetHotel(id);
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
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotelDto = hotelService.GetHotel(id);
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
        public ActionResult DeleteConfirmed(Guid id)
        {
            HotelDTO hotelDto = hotelService.GetHotel(id);
            hotelService.DeleteHotel(hotelDto);
            return RedirectToAction("Index");
        }


        /*/
         * 
         * Бронирование
        public ActionResult MakeBoking(int? id)
{
    try
    {
        RoomDTO room = bookingService.GetRoom(id);
        Mapper.Initialize(cfg => cfg.CreateMap<RoomDTO, OrderViewModel>()
        .ForMember("PhoneId", opt => opt.MapFrom(src => src.Id)));

        var order = Mapper.Map<RoomDTO, OrderViewModel>(phone);
        return View(order);
    }
    catch (ValidationException ex)
    {
        return Content(ex.Message);
    }
}
[HttpPost]
public ActionResult MakeBooking(BookingViewModel booking)
{
    try
    {
        Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
        var bookingDto = Mapper.Map<BookingViewModel, BookingDTO>(order);
        bookingService.MakeBooking(orderDto);
        return Content("<h2>Ваш заказ успешно оформлен</h2>");
    }
    catch (ValidationException ex)
    {
        ModelState.AddModelError(ex.Property, ex.Message);
    }
    return View(order);
}


========================

        // GET: HotelViewModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelViewModel hotelViewModel = db.HotelViewModels.Find(id);
            if (hotelViewModel == null)
            {
                return HttpNotFound();
            }
            return View(hotelViewModel);
        }

        // POST: HotelViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HotelName,HotelStars")] HotelViewModel hotelViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotelViewModel);
        }

        // GET: HotelViewModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelViewModel hotelViewModel = db.HotelViewModels.Find(id);
            if (hotelViewModel == null)
            {
                return HttpNotFound();
            }
            return View(hotelViewModel);
        }

        // POST: HotelViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HotelViewModel hotelViewModel = db.HotelViewModels.Find(id);
            db.HotelViewModels.Remove(hotelViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */
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
/*
public ActionResult MakeBoking(int? id)
{
    try
    {
        RoomDTO room = bookingService.GetRoom(id);
        Mapper.Initialize(cfg => cfg.CreateMap<RoomDTO, OrderViewModel>()
        .ForMember("PhoneId", opt => opt.MapFrom(src => src.Id)));

        var order = Mapper.Map<RoomDTO, OrderViewModel>(phone);
        return View(order);
    }
    catch (ValidationException ex)
    {
        return Content(ex.Message);
    }
}
[HttpPost]
public ActionResult MakeBooking(BookingViewModel booking)
{
    try
    {
        Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
        var bookingDto = Mapper.Map<BookingViewModel, BookingDTO>(order);
        bookingService.MakeBooking(orderDto);
        return Content("<h2>Ваш заказ успешно оформлен</h2>");
    }
    catch (ValidationException ex)
    {
        ModelState.AddModelError(ex.Property, ex.Message);
    }
    return View(order);
}
protected override void Dispose(bool disposing)
{
    bookingService.Dispose();
    base.Dispose(disposing);
}
*/
