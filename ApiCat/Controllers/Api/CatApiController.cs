using ApiCat.Services.CatService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatApiController : Controller
    {

        //private readonly UserManager<ApplicationUser> _userManager;
        //  private readonly IMapper _mapper;
        // private readonly IConfiguration _configuration;
        private readonly ICatService _catService;

        public CatApiController(ICatService catService)
        {
            _catService = catService;
        }

        [HttpGet("FlipCat")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> FlipCat()
        {
            try
            {
                Byte[] result = await _catService.FlipCat();

                return File(result, "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("CatGif")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CatGif()
        {
            try
            {
                Byte[] result = await _catService.CatGif();

                return File(result, "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
