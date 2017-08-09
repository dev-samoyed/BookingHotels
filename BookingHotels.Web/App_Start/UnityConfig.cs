using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.Services;
using BookingHotels.Domain.Interfaces;
using BookingHotels.BLL.Infrastructure;
using BookingHotels.DAL.Repositories;

namespace BookingHotels.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IBookingService, BookingService>();
            container.RegisterType<IHotelService, HotelService>();
            container.RegisterType<IRoomService, RoomService>();

            // from middle layer
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));


             container.RegisterType<IUnitOfWork, EFUnitOfWork>(new InjectionConstructor("DefaultConnection"));

            // было: Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);

        }


    }
}