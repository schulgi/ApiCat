using ApiCat.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiCat.Services.CatService
{
    public class CatService : ICatService
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        private static string catUrl = "https://cataas.com/cat";


        public CatService(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this._configuration = configuration;

        }

        public async Task<Byte[]> FlipCat()
        {
            try
            {
                string uri = catUrl ;
                Byte[] result;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var image = System.Drawing.Image.FromStream(streamReader.BaseStream);
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    result =  (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));
                }
                httpResponse.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Byte[]> CatGif()
        {
            try
            {
                string uri = catUrl + "/gif";
                Byte[] result;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var image = System.Drawing.Image.FromStream(streamReader.BaseStream);
                    
                    result = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));
                }
                httpResponse.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
