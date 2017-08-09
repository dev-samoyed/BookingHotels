using System.Collections.Generic;
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
        private IUserService UserService
        {
            get
            {
                // Поскольку ранее мы зарегистрировали сервис пользователей через контекст OWIN,
                // получаем этот сервис с помощью метода
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

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
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
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
        public async Task<ActionResult> Register(Register model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                // Map recieved data
                var userDto = Mapper.Map<Register, UserDTO>(model);
                userDto.Role = "user";

                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed) {
                    return View("SuccessRegister");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "ad@ad.ad",
                Password = "123123",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}