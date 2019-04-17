using Microsoft.AspNetCore.Http;
using NackowskisApp.DataLayer;
using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NackowskisApp.BusinessLayer
{
    public class AuctionBusiness : IAuctionBusiness
    {
        private IAuctionRepository _repository;

        private IBidRepository _bidRepository;

        


        public AuctionBusiness(IAuctionRepository repository, IBidRepository bidRepository)
        {
            _repository = repository;
            _bidRepository = bidRepository;
        }

        public async Task<bool> CreateAuctionAsync(Auction model)
        {
            var result = await _repository.CreateAuctionAsync(model);

            return result;
        }

        public async Task<bool> DeleteAuctionAsync(int auctionId)
        {

            var hasBids = _bidRepository.GetBidsAsync(auctionId).Result.Count();

            if (hasBids > 0)
            {
                return false;

            }
            else
            {
                var result = await _repository.DeleteAuctionAsync(auctionId);
                return result;
            }


        }

        public Task<Auction> GetAuctionAsync(int auctionId)
        {

            var result =_repository.GetAuctionAsync(auctionId);

            return result;

        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {

            var result = await _repository.GetAuctionsAsync();

            var activeAuctions = result.Where(x=> DateTime.Parse(x.SlutDatum) > DateTime.Now).ToList();

            return activeAuctions;
        }

        public async Task<bool> UpdateAuctionAsync(Auction model)
        {
            var result = await _repository.UpdateAuctionAsync(model);

            return result;
        }

        public async Task<List<Auction>> SearchAuctionsAsync(string searchString)
        {
            var result = await _repository.GetAuctionsAsync();

            var list =result.Where(x => x.Titel.ToUpper().Contains(searchString.ToUpper()) 
            || x.Beskrivning.ToUpper().Contains(searchString.ToUpper())).ToList();

            return list;
        }
    }
}
