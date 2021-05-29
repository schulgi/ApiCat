using ApiCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Services.User
{
    public interface IApplicationUserService
    {
        ApplicationUser GetByEmail(string email);

        
    }
}
