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
            Mapper.Initialize(cfg => {
                cfg.AddProfile<BLLMappingProfile>();
            });
            Mapper.AssertConfigurationIsValid();
            
            ConfigureAuth(app);
        }
    }
}
