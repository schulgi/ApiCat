using ApiCat.Models.Users;
using ApiCat.Services.ApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiCat.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserApiController : Controller
    {

        private readonly IApplicationUserService _applicationUserService;

        public ApplicationUserApiController(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        [HttpGet("UserInFo")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UserInfo()
        {
            try
            {
                var claims = User.Claims.ToList();

                string username = claims.FirstOrDefault(x => x.Type == "Username").Value;

              InfoApplicationUser user =  _applicationUserService.GetUserInfo(username);

                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("Delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var claims = User.Claims.ToList();

                string username = claims.FirstOrDefault(x => x.Type == "Username").Value;

                _applicationUserService.DeleteUserLogin(username);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
