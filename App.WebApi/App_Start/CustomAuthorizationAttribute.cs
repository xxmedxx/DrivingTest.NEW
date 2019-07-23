using App.DataBaseAccess;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;

namespace App.WebApi
{
    public class CustomAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
                return;
            var utility = new UsersUtility();
            if (actionContext.Request.Headers.Authorization == null)
            {                
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("Access Token Required")
                };
                //throw new UnauthorizedAccessException("Access Token Required");
            }
            else
            {
                var token = actionContext.Request.Headers.Authorization.Parameter;
                var user = utility.GetUserNameAndPassword(token, actionContext);
                if (user == null)
                {
                    return;
                }
            }
        }
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }


    }

    public class UsersUtility
    {
        private readonly DrivingTestDBEntities db = new DrivingTestDBEntities();
        //private Repository<AspNetUser> _repo = new Repository<AspNetUser>(db);

        public AspNetUser GetUserNameAndPassword(string token, HttpActionContext actionContext)
        {
            string tokenDecripted = DecriptToken(token, "$TG%FFR%56");
            var separitor = '~';
            var data = tokenDecripted.Split(separitor);//~|~
            if (data.Count() >= 4)
            {
                var username = data[1];
                var password = data[2];
                //var validTimeStamp = DateTime.TryParse(data[2], new CultureInfo("en-US"),new DateTimeStyles(), out timeStamp);
                DateTime? validTimeStamp = Convert.ToDateTime(data[3]); //DateTime.ParseExact(data[2], "MM/dd/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                var tokenUser = JsonConvert.DeserializeObject<AspNetUser>(data[4].Replace(@"\",""));

                //check token is still valid                 
                if (validTimeStamp == null)
                {
                    actionContext.Response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Content = new StringContent("Expired token, wrong date")
                    };
                    return null;
                }
                int daysDiff = ((TimeSpan)(DateTime.Now - validTimeStamp)).Days;
                if (daysDiff > 5)//5 days
                {
                    actionContext.Response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Content = new StringContent("Expired token, old date")
                    };
                    return null;
                    //throw new UnauthorizedAccessException("Expired token");
                }

                //return user from token save trip to DB
                if (tokenUser != null)
                {
                    return tokenUser;
                }

                var user = db.AspNetUsers.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    return user;
                }
            }
            //throw new UnauthorizedAccessException("Invalid token");
            actionContext.Response = new  HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("Invalid token, missing data")
            };
            return null;
        }

        static string DecriptToken(string data, string keyStr)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = { };
                byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
                string encryptKey = "aXb2uy4z"; // MUST be 8 characters
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = new byte[data.Length];
                byteInput = Convert.FromBase64String(data);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateDecryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();
            }
            catch (Exception ex)
            {
                return null;
            }

            Encoding encoding1 = Encoding.UTF8;
            return encoding1.GetString(memStream.ToArray());
        }
    }
}