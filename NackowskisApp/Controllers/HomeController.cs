using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NackowskisApp.BusinessLayer;
using NackowskisApp.Models;
using NackowskisApp.ViewModels;

namespace NackowskisApp.Controllers
{
    public class HomeController : Controller
    {

        private IAuctionBusiness _auctionBusiness;

        public HomeController(IAuctionBusiness auctionBusiness)
        {
            _auctionBusiness = auctionBusiness;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Auctions = _auctionBusiness.GetAuctionsAsync().Result
            };
            return View(model);
        }

    }
}
