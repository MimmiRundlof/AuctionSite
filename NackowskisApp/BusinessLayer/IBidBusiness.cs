using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.BusinessLayer
{
    public interface IBidBusiness
    {
        Task<bool> AddBid(Bid model);

        Task<List<Bid>> GetBidsAsync(int auctionId);

        Task<bool> DeleteBidAsync(int bidId);


    }
}
