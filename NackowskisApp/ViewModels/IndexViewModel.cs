using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.ViewModels
{
    public class IndexViewModel
    {
        public List<Auction> Auctions { get; set; }

        public string SortBy { get; set; }
        

    }
}
