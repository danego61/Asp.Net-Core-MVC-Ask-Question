using AskQuestion.Business.Abstract;
using AskQuestion.Core.Utils;
using AskQuestion.Entities.Concrete;
using AskQuestion.Web.Models;
using AskQuestion.Web.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskQuestion.Web.Controllers
{

    public class LoginController : Controller
    {

        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult SignIn()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            User user = _userService.GetUser(model.UserEmail);
            if (user != null && PasswordHashUtils.IsPasswordMatch(model.UserPassword, user.Password))
            {

                if (user.IsEmailVerified)
                {

                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("UserID", user.UserID.ToString()),
                        new Claim(ClaimTypes.Name, user.NameSurname),
                        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                    };

                    ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new(claimsIdentity);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties()
                        {
                            AllowRefresh = false,
                            ExpiresUtc = DateTime.Now.AddDays(7),
                            IsPersistent = model.RememberMe
                        });
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));

                }
                else
                {
                    _userService.SendVerifyEmail(user);
                    ModelState.AddModelError("", "Email activation link has been resent to your email account. You must click on the link to activate your account!");
                }

            }
            else
                ModelState.AddModelError("", "Email or password incorrect!");
            model.UserPassword = "";
            return View(model);
        }

        public IActionResult SignUp()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserSignUpViewModel model)
        {
            if (_userService.GetUser(model.UserEmail) != null)
                ModelState.AddModelError("", "Email already using!");
            if (model.UserPassword != model.ReUserPassword)
                ModelState.AddModelError("", "Passwords do not match!");
            try
            {
                _userService.AddUser(new User
                {
                    Email = model.UserEmail,
                    Name = model.Name,
                    Surname = model.Surname,
                    Password = model.UserPassword
                });
                return RedirectToAction(nameof(SignIn));
            }
            catch (ValidationException ex)
            {
                foreach (ValidationFailure error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            model.UserPassword = "";
            model.ReUserPassword = "";
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }

        public IActionResult Verify(Guid id)
        {
            _userService.VerifyUserEmail(id);
            return RedirectToAction(nameof(SignIn));
        }

    }

}
