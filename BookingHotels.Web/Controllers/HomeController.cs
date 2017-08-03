using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.DTO;
using BookingHotels.Web.Models;
using AutoMapper;
using BookingHotels.BLL.Infrastructure;
 
namespace BookingHotels.WEB.Controllers
{
    public class HomeController : Controller
    {
        IBookingService bookingService;
        public HomeController(IBookingService serv)
        {
            bookingService = serv;
        }
        public ActionResult Index()
        {
            IEnumerable<RoomDTO> roomDtos = bookingService.GetRooms();
            Mapper.Initialize(cfg => cfg.CreateMap<RoomDTO, RoomViewModel>());
            var rooms = Mapper.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(roomDtos);
            return View(rooms);
        }
        /*
        public ActionResult MakeOrder(int? id)
        {
            try
            {
                PhoneDTO phone = bookingService.GetPhone(id);
                Mapper.Initialize(cfg => cfg.CreateMap<PhoneDTO, OrderViewModel>()
                .ForMember("PhoneId", opt => opt.MapFrom(src => src.Id)));
                var order = Mapper.Map<PhoneDTO, OrderViewModel>(phone);
                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
                var orderDto = Mapper.Map<OrderViewModel, OrderDTO>(order);
                bookingService.MakeOrder(orderDto);
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
    }
}



/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingHotels.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}

    */