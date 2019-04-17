using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NackowskisApp.BusinessLayer;
using NackowskisApp.Models;
using NackowskisApp.ViewModels;
using NackowskisApp.Constants;
namespace NackowskisApp.Controllers
{
    public class AuctionController : Controller
    {
        private IAuctionBusiness _auctionBusiness;

        private IBidBusiness _bidBusiness;

        private UserManager<IdentityUser> _userManager;

        private IUsersBusiness _usersBusiness;


        public AuctionController(IAuctionBusiness auctionBusiness, IBidBusiness bidBusiness, UserManager<IdentityUser> userManager,
            IUsersBusiness usersBusiness)
        {
            _auctionBusiness = auctionBusiness;
            _bidBusiness = bidBusiness;
            _userManager = userManager;
            _usersBusiness = usersBusiness;

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Detail(int auctionId)
        {

            var model = new DetailViewModel()
            {
                Auction = await _auctionBusiness.GetAuctionAsync(auctionId),
                Bids = await _bidBusiness.GetBidsAsync(auctionId)
            };

            model.Bids = model.Bids.OrderByDescending(x => x.Summa).ToList();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBidAsync(DetailViewModel model)
        {

            model.Bid.Budgivare = _userManager.GetUserId(HttpContext.User);

            var result = await _bidBusiness.AddBid(model.Bid);

            if (result)
            {
                return RedirectToAction("Detail", "Auction", new { auctionId = model.Bid.AuktionID });

            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorConstants.BidToLow);


                var newModel = new DetailViewModel()
                {
                    Auction = await _auctionBusiness.GetAuctionAsync(model.Bid.AuktionID),
                    Bids = await _bidBusiness.GetBidsAsync(model.Bid.AuktionID)
                };

                return View("Detail", newModel);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateAuction()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateAuctionAsync(Auction model)
        {

            model.SkapadAv = _userManager.GetUserId(HttpContext.User);
            model.Gruppkod = Constants.Constants.Groupcode;
            model.StartDatum = DateTime.Now.ToString();

            var result = await _auctionBusiness.CreateAuctionAsync(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorConstants.Error);


                return View("CreateAuction");
            }

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuctionAsync(int auctionId)
        {
            bool result = false;

            var userId = _userManager.GetUserId(HttpContext.User);
            if (_usersBusiness.IsAllowed(auctionId, userId))
            {
                result = await _auctionBusiness.DeleteAuctionAsync(auctionId);

            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorConstants.NotAllowed);

                var model = new DetailViewModel()
                {
                    Auction = await _auctionBusiness.GetAuctionAsync(auctionId),
                    Bids = await _bidBusiness.GetBidsAsync(auctionId)
                };

                return View("Detail", model);

            }
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorConstants.CantDeleteAuction);

                var model = new DetailViewModel()
                {
                    Auction = await _auctionBusiness.GetAuctionAsync(auctionId),
                    Bids = await _bidBusiness.GetBidsAsync(auctionId)
                };
                return View("Detail", model);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateAuctionAsync(int auctionId)
        {

            var model = new DetailViewModel()
            {
                Auction = await _auctionBusiness.GetAuctionAsync(auctionId),
                Bids = await _bidBusiness.GetBidsAsync(auctionId)
            };

            var userId = _userManager.GetUserId(HttpContext.User);

            if (_usersBusiness.IsAllowed(auctionId, userId))
            {

                return View("Edit", model.Auction);
            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorConstants.NotAllowed);

                return View("Detail", model);
            }
                       
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateAuctionAsync(Auction model)
        {

            var result = await _auctionBusiness.UpdateAuctionAsync(model);

            return RedirectToAction("Detail", new { auctionId = model.AuktionID });
                       
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SearchAuctionAsync(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                var model = new IndexViewModel()
                {
                    Auctions = await _auctionBusiness.SearchAuctionsAsync(searchString)
                };
                return View("Search", model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBidAsync(int bidId)
        {

            var result = _bidBusiness.DeleteBidAsync(bidId);

            return RedirectToAction("Index", "Home");

        }


    }

}