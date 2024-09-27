using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager , SignInManager <AppUser>
        signInManager)
        {
             _signInManager = signInManager; //password of user from the database
            _userManager = userManager; //get the user at database 
           
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
                Token = "This will be a token",
                DisplayName = user.DisplayName


            };
        
        }

        
    }
}