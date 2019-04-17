using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NackowskisApp.BusinessLayer;
using NackowskisApp.Models;
using NackowskisApp.ViewModels;

namespace NackowskisApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IUsersBusiness _usersBusiness;

        private IAuctionBusiness _auctionBusiness;

        private IChartBusiness _chartBusiness;

        private UserManager<IdentityUser> _userManager;


        public AdminController(IUsersBusiness usersBusiness, IAuctionBusiness auctionBusiness, IChartBusiness chartBusiness,
            UserManager<IdentityUser> userManager)
        {
            _usersBusiness = usersBusiness;
            _auctionBusiness = auctionBusiness;
            _chartBusiness = chartBusiness;
            _userManager = userManager;
        }

        public IActionResult GetUsers()
        {

            var model = new ManageUsersViewModel
            {
                Users = new SelectList(_usersBusiness.GetUsers(), "Id", "UserName"),
                Roles = new SelectList(_usersBusiness.GetRoles(), "Id", "NormalizedName")
            };

            return View("ManageUsers", model);
        }

        public async Task<IActionResult> ChangeUserRole(string userId, string roleId)
        {

            var test = await _usersBusiness.EditUserRole(userId, roleId);


            return RedirectToAction("Index", "Home");
        }

        public IActionResult Chart(ChartViewModel model)
        {

            if (model.Month == null)
            {
                model.Month = DateTime.Now.ToString("MMMM");
            }

            model.UserId = _userManager.GetUserId(HttpContext.User);


            model.Chart = _chartBusiness.GetValuesForChart(model.UserId, model.OnlyOwnedAuctions, model.Month);

            model.Months = new SelectList(_auctionBusiness.GetAuctionsAsync().Result.Select(x => DateTime.Parse(x.StartDatum).ToString("MMMM")).Distinct());


            return View(model);

        }

  


    }
}