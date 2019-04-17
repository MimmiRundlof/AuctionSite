using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.BusinessLayer
{
    public interface IUsersBusiness
    {
        List<IdentityUser> GetUsers();

        List<IdentityRole> GetRoles();

        Task<bool> EditUserRole(string userId, string roleId);

        bool IsAllowed(int auctionId, string userId);
    }
}
