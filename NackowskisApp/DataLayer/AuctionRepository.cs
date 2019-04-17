using Microsoft.AspNetCore.Mvc;
using NackowskisApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace NackowskisApp.DataLayer
{
    public class AuctionRepository : IAuctionRepository
    {
        private HttpClient _client;

        public AuctionRepository()
        {
            //_client = httpClient;
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://nackowskis.azurewebsites.net")
            };

        }
        public async Task<bool> CreateAuctionAsync(Auction model)
        {

            var jsonInString = JsonConvert.SerializeObject(model);

            var stringContent = new StringContent(jsonInString, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/api/Auktion", stringContent);

            return response.IsSuccessStatusCode;

        }
        public async Task<Auction> GetAuctionAsync(int auctionId)
        {

            HttpResponseMessage response = await _client.GetAsync("/api/Auktion/1310/" + auctionId);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Auction));

            Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            Auction model = (Auction)serializer.ReadObject(responseStream);

            return model;

        }

        public async Task<List<Auction>> GetAuctionsAsync()
        {

            HttpResponseMessage response = await _client.GetAsync("/api/Auktion/1310/");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Auction>));

            Stream responseStream = response.Content.ReadAsStreamAsync().Result;

            List<Auction> model = (List<Auction>)serializer.ReadObject(responseStream);

            return model;


        }


        public async Task<bool> DeleteAuctionAsync(int auctionId)
        {

            HttpResponseMessage response = await _client.DeleteAsync("/api/Auktion/1310/" + auctionId);


            return response.IsSuccessStatusCode;

        }

        public async Task<bool> UpdateAuctionAsync(Auction model)
        {

            var jsonInString = JsonConvert.SerializeObject(model);

            var stringContent = new StringContent(jsonInString, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync("/api/Auktion/1310/" + model.AuktionID, stringContent);

            return response.IsSuccessStatusCode;

        }
    }
}
