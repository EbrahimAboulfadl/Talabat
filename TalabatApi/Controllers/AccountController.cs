using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using TalabatApi.DTOs;
using TalabatApi.Extensions;

namespace TalabatApi.Controllers
{

    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager ,
                                SignInManager<AppUser> signInManager ,
                                ITokenService tokenService,
                                IMapper mapper
                                )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        
            {
            if ( CheckEmailExistanceAsync(model.Email).Result.Value)
                return BadRequest("There is already account with this email");

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

       
        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser() {

            var userEmail =  User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(userEmail);

            var userDto = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            };
            return Ok( userDto );

        
        }


        [HttpGet("GetCurrentUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {


            var user = await userManager.FindUserWithAddressAsync(User);
            var addressDto = mapper.Map<Address, AddressDto>(user.Address);
            return Ok(addressDto);


        }   
        [HttpPut("UpdateCurrentUserAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress( AddressDto addressDto)
        {

            var user = await userManager.FindUserWithAddressAsync(User);
            var mappedAddress = mapper.Map<AddressDto, Address>(addressDto);
            mappedAddress.Id = user.Address.Id;
            user.Address = mappedAddress;
            var result = await userManager.UpdateAsync(user);
            if(result.Succeeded)
            return Ok(addressDto);
            return BadRequest();


        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistanceAsync(string email) { 
            var user = await userManager.FindByEmailAsync(email);
            return (user is not null);
        
        
        }
    }
}
