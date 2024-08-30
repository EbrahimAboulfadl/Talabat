using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbSeed
    {
        public static async Task UserSeedAsync(UserManager<AppUser> manager) {

            if (!manager.Users.Any()) { 
            
                var user = new AppUser() { 
                    DisplayName = "Ebrahim Aboulfadl",
                UserName = "Abou",
                Email = "xmrhimax@gmail.com"
                ,PhoneNumber = "01014621673"
                };
               await manager.CreateAsync(user,"Pa$$w0rd") ;
            }

        }
    }
}
