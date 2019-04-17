using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NackowskisApp.BusinessLayer
{
    public interface IAuctionBusiness
    {
        Task<bool> CreateAuctionAsync(Auction model);

        Task<Auction> GetAuctionAsync(int auctionId);

        Task<List<Auction>> GetAuctionsAsync();

        Task<bool> UpdateAuctionAsync(Auction model);

        Task<bool> DeleteAuctionAsync(int auctionId);

        Task<List<Auction>> SearchAuctionsAsync(string search);

    }
}
