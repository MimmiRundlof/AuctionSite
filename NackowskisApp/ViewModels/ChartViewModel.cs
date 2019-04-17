using Microsoft.AspNetCore.Mvc.Rendering;
using NackowskisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.ViewModels
{
    public class ChartViewModel
    {
        public SelectList Months { get; set; }

        public string Month { get; set; }

        public bool OnlyOwnedAuctions { get; set; }

        public Chart Chart { get; set; }

        public string UserId { get; set; }

    }
}
