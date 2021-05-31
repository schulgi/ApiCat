using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Services.CatService

{
    public interface ICatService
    {
        Task<Byte[]> FlipCat();
        Task<Byte[]> CatGif();
    }
}
