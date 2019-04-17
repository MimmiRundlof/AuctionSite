using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NackowskisApp.BusinessLayer;
using NackowskisApp.Constants;
using NackowskisApp.Data;
using NackowskisApp.ViewModels;

namespace NackowskisApp.Controllers
{

    public class UserController : Controller
    {

        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        private IUsersBusiness _usersBusiness;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUsersBusiness usersBusiness)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usersBusiness = usersBusiness;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userExists = _usersBusiness.GetUsers().Any(x => x.UserName == model.Username);

                if (userExists)
                {
                    return Json(new { userExist = true });

                }

                var user = new IdentityUser { UserName = model.Username, Email = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Constants.Constants.RegularUser);
                    await _signInManager.PasswordSignInAsync(model.Username, model.Password, lockoutOnFailure: false, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

            }
            return Json(new { stateInvalid = true });

        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

     
    }
}