using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NackowskisApp.Data;

namespace NackowskisApp.DataLayer
{
    public class UsersRepository : IUsersRepository
    {
        private ApplicationDbContext _context;

        private UserManager<IdentityUser> _userManager;

        public UsersRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> EditUserRole(string userId, string roleId)
        {
            var user = _userManager.Users.Where(x => x.Id == userId).SingleOrDefault();

            var role = _context.Roles.Where(x => x.Id == roleId).SingleOrDefault();

            var currentRole = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRole);

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            

            return result.Succeeded;
        }

        public List<IdentityRole> GetRoles()
        {
            var roles = _context.Roles;

            return roles.ToList();
        }

        public List<IdentityUser> GetUsers()
        {
            var users = _context.Users;

            return users.ToList();

        }
    }
}
