using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.DataLayer
{
    public interface IUsersRepository
    {
        List<IdentityUser> GetUsers();

        List<IdentityRole> GetRoles();

        Task<bool> EditUserRole(string userId, string roleId);


    }
}
