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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CallCat()
        {

           string uri = "https://cataas.com/cat";
            Image result;
            ApiCat.Models.CatModel model = new Models.CatModel();


            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Image image = System.Drawing.Image.FromStream(streamReader.BaseStream);
                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                model.CatUrl = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));
            }
            httpResponse.Close();


            return View("FlipCat", model);
        }
    }
}
    
