using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace TalabatApi.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager <AppUser>  userManager , ClaimsPrincipal user) { 


            var userWithAddress = await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email == user.FindFirstValue(ClaimTypes.Email));

            return userWithAddress;
        
        
        }

    }
}
