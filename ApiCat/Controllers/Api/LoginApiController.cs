using ApiCat.Models;
using ApiCat.Models.Users;
using ApiCat.Services.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginApiController(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration )
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(ApiSignupUser userSignUp)
        {
            var user = _mapper.Map<ApiSignupUser, ApplicationUser>(userSignUp);
            user.UserName = user.Email;
            user.LastLogin = DateTime.Now;

            var userCreateResult = await _userManager.CreateAsync(user, userSignUp.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<UserToken>> SignIn(ApiLoginUser userLoginResource)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
                
                if (user is null)
                {
                    return NotFound("User not found");
                }

                var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);
             
                if (userSigninResult)
                {
                    return BuildToken(user);
                }

                return BadRequest("Email or password incorrect.");
            }
            else
            {
                return BadRequest("Invalid model");
            }
        }

        private UserToken BuildToken(ApplicationUser userInfo)
        {
            var claims = new List<Claim>
            {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim ("Id", userInfo.Id.ToString()),
        new Claim ("Username", userInfo.Email)
    };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}
