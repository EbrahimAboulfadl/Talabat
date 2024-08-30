using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace TalabatApi.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication(options=> {


                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;



                }).AddJwtBearer(options => {

                options.TokenValidationParameters = new() { 
                    ValidateIssuer = true , 
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true ,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime = true ,
                    ValidateIssuerSigningKey = true ,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),


                };
                });
            return services;
        }
    }
}
