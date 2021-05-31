using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Controllers.Api
{
    public class AplicattionUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
