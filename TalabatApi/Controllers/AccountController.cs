using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using TalabatApi.DTOs;

namespace TalabatApi.Controllers
{

    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<AppUser> userManager ,
                                SignInManager<AppUser> signInManager ,
                                ITokenService tokenService )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        
            {

            var user = new AppUser() { 
            DisplayName = model.DisplayName,
            Email = model.Email,
            UserName = model.Email.Split('@')[0],
            PhoneNumber = model.Phone
            };
            
           var result =  await userManager.CreateAsync(user,model.Password);

            return result.Succeeded ? new UserDto() { 
                DisplayName = user.DisplayName
                ,Email = model.Email
                , Token = await tokenService.CreateTokenAsync(user,userManager)
            
            } : BadRequest();




            }


        //Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)

        {

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(401);
            var response = await signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (!response.Succeeded) return Unauthorized(401);
             return Ok( new UserDto() { 
                                        DisplayName = user.DisplayName , 
                                        Email = user.Email , 
                                        Token = await tokenService.CreateTokenAsync(user, userManager)
             }
                 );
           


            



        }
    }
}
