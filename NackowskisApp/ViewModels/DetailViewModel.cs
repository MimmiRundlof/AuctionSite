using Microsoft.AspNetCore.Identity;
using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.ViewModels
{
    public class DetailViewModel
    {
        public Auction Auction { get; set; }

        public List<Bid> Bids { get; set; }

        public Bid Bid { get; set; }

    }
}
