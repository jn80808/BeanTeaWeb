using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager , SignInManager <AppUser>
        signInManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _signInManager = signInManager; //password of user from the database
            _tokenService = tokenService;
            _userManager = userManager; //get the user at database 
           
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

           var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

              return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token =_tokenService.CreateToken(user),
                    Email = user.Email
                };
        }

        [HttpGet("emailExists")]

        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email)!= null;
        }

        //Get 
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(User);

            return _mapper.Map<Address,AddressDto>(user.Address);
        }

        //Update Address
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);

            user.Address = _mapper.Map<AddressDto,Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address,AddressDto>(user.Address));

            return BadRequest ("Problem updating the user");
        
        }
        
        
    

        [HttpPost("login")]
        public async Task<ActionResult<Dtos.UserDto>> Login ( LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email); // check the user email inputed in database 

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password, false);// check the user email inputed in database 

            if (!result.Succeeded) return Unauthorized (new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(
            RegisterDto registerDto)
            {
                var user = new AppUser
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded) return BadRequest(new ApiResponse(400));

                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token =_tokenService.CreateToken(user),
                    Email = user.Email
                };

            }
        

            

        
    }
}