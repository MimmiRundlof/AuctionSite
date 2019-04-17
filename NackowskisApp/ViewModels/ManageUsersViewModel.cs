using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.ViewModels
{
    public class ManageUsersViewModel
    {
        public SelectList Users { get; set; }

        public SelectList Roles { get; set; }

    }
}
