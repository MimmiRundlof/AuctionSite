using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NackowskisApp.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.BusinessLayer
{
    public class UsersBusiness : IUsersBusiness
    {
        private IUsersRepository _usersRepository;

        private UserManager<IdentityUser> _userManager;

        private IAuctionBusiness _auctionBusiness;

        public UsersBusiness(IUsersRepository usersRepository, UserManager<IdentityUser> userManager, IAuctionBusiness auctionBusiness)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
            _auctionBusiness = auctionBusiness;
        }

        public async Task<bool> EditUserRole(string userId, string roleId)
        {
            return await _usersRepository.EditUserRole(userId, roleId);
            
        }

        public List<IdentityRole> GetRoles()
        {
            var result = _usersRepository.GetRoles();

            return result;
        }

        public List<IdentityUser> GetUsers()
        {
            var result = _usersRepository.GetUsers();


            return result;
        }

        public bool IsAllowed(int auctionId, string userId)
        {
            var model = _auctionBusiness.GetAuctionAsync(auctionId).Result;

            var createdBy = model.SkapadAv;

            bool result = userId.ToString() == createdBy.ToString();

            return result;
        }
    }
}
