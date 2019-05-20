using App.IRepository;
using App.DataBaseAccess;
using System.Collections.Generic;
using System.Web.Http;
using App.Models;
using AutoMapper;
using System.Linq;

namespace Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IRepository<AspNetUser> repo;
        public UsersController(IRepository<AspNetUser> repo)
        {
            this.repo = repo;
        }
        // GET: api/USers
        [Route("api/users")]
        [HttpGet]
        public IEnumerable<AppUserModel> Users()
        {
            var users = repo.GetQueryableSet().ToList() ;
            return Mapper.Map<IEnumerable<AppUserModel>>(users);
        }

        // GET: api/USers/5
        public AppUserModel Get(string id)
        {
            var user = repo.GetById(id);
            return Mapper.Map<AppUserModel>(user);
        }

        // POST: api/USers
        [Route("api/new/")]
        [HttpPost]
        public AppUserModel Post([FromBody]AppUserModel value)
        {
            if (value == null)
                return null;
            //or will use try catch to get the exception if user alredy exists!
            if (repo.GetQueryableSet().FirstOrDefault(u => u.Email == value.Email) != null)
                return null;

            var obj = Mapper.Map<AspNetUser>(value);
            var user = repo.Insert(obj);
            user.EmailConfirmed = false;
            return Mapper.Map<AppUserModel>(user);
        }

        // PUT: api/USers/5
        public AppUserModel Put(string id, [FromBody]AppUserModel value)
        {
            var obj = Mapper.Map<AspNetUser>(value);
            var user = repo.Update(obj);
            return Mapper.Map<AppUserModel>(user);
        }

        // DELETE: api/USers/5
        public int Delete(string id)
        {
            return repo.Delete(id);
        }
        [Route("api/login/")]
        [HttpPost]
        public AppUserModel Login([FromBody]AppUserModel value)
        {
            if (value == null)
                return null;
            var user = repo.GetQueryableSet().FirstOrDefault(u => u.Email == value.Email);
            if (user != null)
                return Mapper.Map<AppUserModel>(user);

            return null;
        }
    }
}
