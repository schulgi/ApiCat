using ApiCat.Models;
using ApiCat.Services.ApplicationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiCat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,IApplicationUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _applicationUserService = userService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                var test = HttpContext.User.Identities;
            //    var user = await _userManager.GetUserAsync(HttpContext.User);

                //    await _userManager.AddClaimAsync(user, new Claim("Email", user.Email));

                //    var test = User.Claims.ToList();


                //    _applicationUserService.SaveUserLogin(user.Email);


                //    return RedirectToAction("Index", "Cat");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
