using AutoMapper;
using BookingHotels.BLL;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(BookingHotels.Web.Startup))]
namespace BookingHotels.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Add Automapper profile
            Mapper.Initialize(cfg => {
                cfg.AddProfile<BLLMappingProfile>();
                cfg.AddProfile<WebMappingProfile>();
            });
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            // Dry run all configured type maps
            Mapper.AssertConfigurationIsValid();
            // Add Unity MVC 5 Register Componets 
            UnityConfig.RegisterComponents();
        }
    }
}
 