using AutoMapper;
using BookingHotels.BLL;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using BookingHotels.BLL.Services;
using Microsoft.AspNet.Identity;
using BookingHotels.BLL.Interfaces;

[assembly: OwinStartupAttribute(typeof(BookingHotels.Web.Startup))]
namespace BookingHotels.Web
{
    public partial class Startup
    {
        // С помощью фабрики сервисов здесь создается сервис для работы с сервисами:
        IServiceCreator serviceCreator = new ServiceCreator();



        public void Configuration(IAppBuilder app)
        {
            // Add Automapper profile
            Mapper.Initialize(cfg => {
                cfg.AddProfile<BLLMappingProfile>();
                cfg.AddProfile<WebMappingProfile>();
            });



            // Потом сервис региструется контекстом OWIN:
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            // Dry run (прогон) all configured type maps
            Mapper.AssertConfigurationIsValid();
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }




            //ConfigureAuth(app);
    }
}
 