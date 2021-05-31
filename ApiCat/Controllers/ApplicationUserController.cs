using ApiCat.Models;
using ApiCat.Models.Users;
using ApiCat.Services.ApplicationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Controllers.Api
{
    public class ApplicationUserController : Controller
    {


        private readonly IApplicationUserService _applicationUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(IApplicationUserService applicationUserService, UserManager<ApplicationUser> userManager)
        {
            _applicationUserService = applicationUserService;
            _userManager = userManager;
        }


        public async Task<IActionResult> UserInfo()
        {
            InfoApplicationUser model = new InfoApplicationUser();
            if (User.Identity.IsAuthenticated)
            {
                var user =  await _userManager.GetUserAsync(HttpContext.User);

                model = _applicationUserService.GetUserInfo(user.Email);


                return View("MyData", model);
            }
            return View();
        }


    }
}
