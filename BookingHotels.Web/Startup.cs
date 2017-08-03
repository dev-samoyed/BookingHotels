using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookingHotels.Web.Startup))]
namespace BookingHotels.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
