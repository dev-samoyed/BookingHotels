using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using BookingHotels.BLL.DTO;
using System.Security.Claims;
using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.Infrastructure;
using AutoMapper;
using BookingHotels.Domain.Identity;

namespace BookingHotels.Controllers
{
    public class AccountController : Controller
    {

        IUserService userService;
        public AccountController(IUserService serv)
        {
            userService = serv;
        }


        //private IUserService UserService
        //{
        //    // Get User Service registred through Owin context
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<IUserService>();
        //    }
        //}

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(CustomUserLogin model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO {
                    Email = model.Email, 
                    Password = model.Password
                };
                ClaimsIdentity claim = await userService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Hotel");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Hotel");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(CustomUserRegister model)
        {
            if (ModelState.IsValid)
            {
                // Map recieved data to Dto and send it to user service
                var userDto = Mapper.Map<CustomUserRegister, UserDTO>(model);
                userDto.Role = "user";

                OperationDetails operationDetails = await userService.Create(userDto);
                if (operationDetails.Succedeed) {
                    return View("SuccessRegister");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
    }
}