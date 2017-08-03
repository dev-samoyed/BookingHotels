using BookingHotels.BLL.Services;
using System.Web.Mvc;
using Ninject;
using System.Collections.Generic;
using System;
using BookingHotels.BLL.Interfaces;
 
namespace BookingHotels.WEB.Util
{
    // Интерфейс IOrderService сопоставляется с классом OrderService.
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IBookingService>().To<BookingService>();
        }
    }
}