using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NackowskisApp.DataLayer;
using NackowskisApp.Models;

namespace NackowskisApp.BusinessLayer
{
    public class BidBusiness : IBidBusiness
    {
        IBidRepository _repository;

        IAuctionRepository _auctionRepository;


        public BidBusiness(IBidRepository repository, IAuctionRepository auctionRepository)
        {
            _repository = repository;
            _auctionRepository = auctionRepository;

        }

        public async Task<bool> AddBid(Bid model)
        {
            int currentMaxBid;

            var bids = _repository.GetBidsAsync(model.AuktionID).Result;

            if (bids.Count() < 1)
            {
                var auction = await _auctionRepository.GetAuctionAsync(model.AuktionID);

                currentMaxBid = auction.Utropspris;
            }
            else
            {
                var maxBid = bids.Select(x => x.Summa).Max();
                currentMaxBid = maxBid;

            }

            if (model.Summa > currentMaxBid)
            {

                return await _repository.AddBid(model);
            }
            else
            {
                return false;
            }

        }

        public Task<bool> DeleteBidAsync(int bidId)
        {
            return _repository.DeleteBidAsync(bidId);
        }

        public Task<List<Bid>> GetBidsAsync(int auctionId)
        {
            return _repository.GetBidsAsync(auctionId);
        }
    }
}
