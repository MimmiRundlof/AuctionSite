using Microsoft.AspNetCore.Mvc;
using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NackowskisApp.DataLayer
{
    public interface IAuctionRepository
    {
        Task<bool> CreateAuctionAsync(Auction model);

        Task<Auction> GetAuctionAsync(int auctionId);

        Task<List<Auction>> GetAuctionsAsync();

        Task<bool> UpdateAuctionAsync(Auction model);

        Task<bool> DeleteAuctionAsync(int auctionId);



    }
}
