using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWA.BL.BLModels;
using RWA.BL.Repositories;
using System.Security.Claims;
using WebApp.Models.ViewModels;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public AuthController(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginVM userLogin)
        {
            var user = _userRepo.Login(_mapper.Map<BlUser>(userLogin));
            if (user != null)
            {
                UserVM userVM = _mapper.Map<UserVM>(user);
                var claims = new List<Claim>() {
                    new Claim("Id", user.Iduser.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                Task.Run(async () =>
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties)
                ).GetAwaiter().GetResult();

                if (user.Role.Name == "Admin")
                    return RedirectToAction("AdminSearch", "Project");
                else
                    return RedirectToAction("Search", "Project");
            }
            else
            {
                ModelState.AddModelError(nameof(UserLoginVM.Password), "Incorrect credentials");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserVM userRegister)
        {
            try
            {
                BlUser user = _mapper.Map<BlUser>(userRegister);
                _userRepo.Add(user);
                return RedirectToAction(nameof(RegisterSuccess));
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.Message.StartsWith("Violation of UNIQUE KEY constraint "))
                {
                    ModelState.AddModelError(nameof(UserVM.Username), "That username is taken");
                }
                return View();
            }
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSuccess(IFormCollection value)
        {
            return RedirectToAction(nameof(Login));
        }

        [Authorize(Roles = "Admin, Volunteer")]
        public ActionResult Logout()
        {
            Task.Run(async () =>
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme)
            ).GetAwaiter().GetResult();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin, Volunteer")]
        [HttpPut]
        public ActionResult ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (_userRepo.ChangePassword(changePasswordModel.UserName, changePasswordModel.Password, changePasswordModel.NewPassword)) {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        /*public ActionResult Forbidden()
        {
            return View();
        }*/
    }
}
