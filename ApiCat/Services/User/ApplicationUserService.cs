using ApiCat.Data;
using ApiCat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Services.User
{
    public class ApplicationUserService : IApplicationUserService
    {

        private readonly ApplicationDbContext context;
       

        public ApplicationUserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ApplicationUser GetByEmail (string email)
        {
            try
            {
                ApplicationUser user = new ApplicationUser();

                user = context.Users.FirstOrDefault(x => x.Email == email);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
