using AutoMapper;
using BookingHotels.BLL;
using Microsoft.Owin;
using Owin;

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
            // Dry run (прогон) all configured type maps
            Mapper.AssertConfigurationIsValid();
            
            ConfigureAuth(app);
        }
    }
}
