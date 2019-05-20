using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.DataBaseAccess;
using App.IRepository;

namespace Api.Services
{
    public class UsersSecurity
    {
        private readonly IRepository<AspNetUser> repo;
        public UsersSecurity(IRepository<AspNetUser> repo)
        {
            this.repo = repo;
        }
        public static bool ValidateUser(string username, string password)
        {
            return false;
        }
    }
}