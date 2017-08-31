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
using BookingHotels.Web.Controllers;

namespace BookingHotels.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUserService serv) : base(serv)
        { }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(CustomUserLogin model)
        {
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
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map recieved data to Dto and send it to user service
                var userDto = Mapper.Map<RegisterViewModel, UserDTO>(model);
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