using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private  UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var user = new IdentityUser()
            {
                UserName = request.UserName
            };

            await _userManager.CreateAsync(user, "password");

            var claim = new Claim("Role", "Admin");            

            await _userManager.AddClaimAsync(user, claim);
            return true;

        }
    }
}
