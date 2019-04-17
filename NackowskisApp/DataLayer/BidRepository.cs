using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using NackowskisApp.Models;
using Newtonsoft.Json;

namespace NackowskisApp.DataLayer
{
    public class BidRepository : IBidRepository
    {
        private HttpClient _client;

        public BidRepository()
        {
            //_client = client;

            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://nackowskis.azurewebsites.net")
            };
        }


        public async Task<bool> AddBid(Bid model)
        {
            var jsonInString = JsonConvert.SerializeObject(model);

            var stringContent = new StringContent(jsonInString, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/api/Bud/1310/" + model.AuktionID, stringContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBidAsync(int bidId)
        {
            HttpResponseMessage response = await _client.DeleteAsync("/api/Bud/1310/" + bidId);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Bid>> GetBidsAsync(int auctionId)
        {
            HttpResponseMessage response = await _client.GetAsync("/api/Bud/1310/" + auctionId.ToString());

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Bid>));

            Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            List<Bid> model = (List<Bid>)serializer.ReadObject(responseStream);

            return model;
        }
    }
}
