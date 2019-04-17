using Microsoft.AspNetCore.Mvc.Rendering;
using NackowskisApp.DataLayer;
using NackowskisApp.Models;
using NackowskisApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.BusinessLayer
{
    public class ChartBusiness : IChartBusiness
    {
        private IAuctionRepository _auctionRepository;

        private IBidRepository _bidRepository;

        public ChartBusiness(IAuctionRepository auctionRepository, IBidRepository bidRepository)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
        }

        public Chart GetValuesForChart(string userId, bool onlyOwnedAuctions, string month)
        {

            var allAuctions = _auctionRepository.GetAuctionsAsync().Result;

            //Gets auctions from chosen month
            allAuctions = allAuctions.Where(x => DateTime.Parse(x.StartDatum).ToString("MMMM") == month).ToList();

            if (onlyOwnedAuctions)
            {
                allAuctions = allAuctions.Where(x => x.SkapadAv.ToString() == userId.ToString()).ToList();
            }

            var sumOfFinalBids = 0;

            foreach (var item in allAuctions)
            {
                var bids = _bidRepository.GetBidsAsync(item.AuktionID).Result;

                sumOfFinalBids = sumOfFinalBids + (bids.OrderByDescending(x => x.Summa).Select(x => x.Summa).FirstOrDefault());

            }

            var sumOfStartingBids = allAuctions.Select(x => x.Utropspris).Sum();

            var chartArray = CalculateChart(sumOfStartingBids, sumOfFinalBids);

            var chartModel = new Chart()
            {
                StartingBids = chartArray[0],
                FinalBids = chartArray[1],
                Difference = chartArray[2]
            };
            

            return chartModel;
        }

        public int[] CalculateChart(int startingBids, int finalBids)
        {

            var difference = finalBids - startingBids;

            return new[] { startingBids, finalBids, difference };

        }
    }
}
