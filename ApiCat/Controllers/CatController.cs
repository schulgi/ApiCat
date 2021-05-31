using ApiCat.Services.CatService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiCat.Controllers
{
    public class CatController : Controller
    {


        private readonly ICatService _catService;

        public CatController(ICatService catService)
        {
            _catService = catService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CallCat()
        {
            ApiCat.Models.CatModel model = new Models.CatModel();

            model.CatUrl = await _catService.FlipCat();
            
            return View("FlipCat", model);
        }
    }
}
    
