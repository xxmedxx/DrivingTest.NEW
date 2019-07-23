using App.IRepository;
using App.DataBaseAccess;
using System.Collections.Generic;
using System.Web.Http;
using App.Models;
using AutoMapper;
using System.Linq;
using System;
using System.Text;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Web.Http.Cors;
using System.Net;
using System.Net.Http;

namespace App.WebApi.Controllers
{
    [CustomAuthorization]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [AllowAnonymous]
        [Route("api/register")]
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
        [HttpPut]
        public AppUserModel Put(string id, [FromBody]AppUserModel value)
        {
            var obj = Mapper.Map<AspNetUser>(value);
            var user = repo.Update(obj);
            return Mapper.Map<AppUserModel>(user);
        }

        // DELETE: api/USers/5
        [HttpDelete]
        public int Delete(string id)
        {
            return repo.Delete(id);
        }

        //Login: api/login
        [AllowAnonymous]
        [Route("api/login")]
        [HttpGet]
        public IHttpActionResult Login([FromUri]AppUserModel value)
        {
            if (value == null)
                return BadRequest("User is null");

            var user = repo.GetQueryableSet().FirstOrDefault(u => u.Email == value.Email);
            if (user != null)
            {
                var token = "~" +  user.Email + "~" + user.PasswordHash + "~" + DateTime.Now.ToString("MM/dd/yy HH:mm:ss")+ "~";
                token += JsonConvert.SerializeObject(Mapper.Map<AppUserModel>(user)) + "~";
                return Ok(EncriptToken(token, user.Email));
            }
            return NotFound();
        }

        private string HashText(string text, string salt, HashAlgorithmType hasher)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedHash = Encoding.UTF8.GetBytes(string.Concat(text, salt));

                return Convert.ToBase64String(sha256.ComputeHash(combinedHash));
            }
        }

        private string EncriptToken(string data,string keyStr)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = { };
                byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
                string encryptKey = "aXb2uy4z"; // MUST be 8 characters
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = Encoding.UTF8.GetBytes(data);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateEncryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
                return null;
            }
            return Convert.ToBase64String(memStream.ToArray());
        }
    }
}
