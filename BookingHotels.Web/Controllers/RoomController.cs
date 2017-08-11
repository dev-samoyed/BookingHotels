using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookingHotels.Web.Models;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.DTO;
using AutoMapper;
using System.Net;

namespace BookingHotels.Web.Controllers
{
    public class RoomController : Controller
    {
        IRoomService roomService;
        IHotelService hotelService;
        public RoomController(IRoomService serv, IHotelService hotelServ)
        {
            roomService = serv;
            hotelService = hotelServ;
        }

        // GET: Room/Index
        public ActionResult Index()
        {
            IEnumerable<RoomDTO> roomDtos = roomService.GetRooms();
            // Map DTO to ViewModel using Dtos data
            var rooms = Mapper.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(roomDtos);
            return View(rooms);
        }
        // GET: Room/Details/{Guid}
        public ActionResult Details(Guid? id)
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
            ViewBag.hotels = new SelectList(hotels, "Id", "HotelName");
            
            return View(roomViewModel);
        }

        
        //// GET: RoomViewModels/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RoomViewModel roomViewModel = db.RoomViewModels.Find(id);
        //    if (roomViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(roomViewModel);
        //}

        //// POST: RoomViewModels/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,HotelId,Price,RoomType")] RoomViewModel roomViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(roomViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(roomViewModel);
        //}

        //// GET: RoomViewModels/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RoomViewModel roomViewModel = db.RoomViewModels.Find(id);
        //    if (roomViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(roomViewModel);
        //}

        //// POST: RoomViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    RoomViewModel roomViewModel = db.RoomViewModels.Find(id);
        //    db.RoomViewModels.Remove(roomViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
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
