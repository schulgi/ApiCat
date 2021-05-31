using ApiCat.Data;
using ApiCat.Models;
using ApiCat.Models.Users;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCat.Services.ApplicationService
{
    public class ApplicationUserService : IApplicationUserService
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        

        public ApplicationUserService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        public InfoApplicationUser GetUserInfo(string email)
        {
            try
            {

                InfoApplicationUser user = new InfoApplicationUser();

               var MyUser = context.Users.FirstOrDefault(x => x.Email == email);

                if (MyUser != null)
                {
                    user = mapper.Map<InfoApplicationUser>(MyUser);
                }

                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUserLogin(string email)
        {
            try
            {
                ApplicationUser user = GetByEmail(email);

                user.LastLogin = DateTime.Now;

                context.Attach(user);
                context.Entry(user).Property("LastLogin").IsModified = true;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUserLogin(string email)
        {
            try
            {
                ApplicationUser user = GetByEmail(email);

                if (user != null)
                {
                    context.Entry(user).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
