using ApiCat.Models;
using ApiCat.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Services.ApplicationService
{
    public interface IApplicationUserService
    {
        ApplicationUser GetByEmail(string email);
        InfoApplicationUser GetUserInfo(string email);
        void SaveUserLogin(string user);
        void DeleteUserLogin(string email);


    }
}
