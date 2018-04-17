using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StudyCore.NetNote.Middleware.Basic
{
    public class BasicMiddleware
    {
        private readonly RequestDelegate _next;

        public const string AuthorizationHeather = "Authorization";

        public const string WWWAuthorizationHeather = "WWW-Authenticate";

        private BasicUser _user;

        public BasicMiddleware(RequestDelegate next, BasicUser user)
        {
            this._next = next;
            this._user = user;
        }



        public Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;
            string auth = request.Headers[AuthorizationHeather];
            if (string.IsNullOrEmpty(auth))
            {
                return BasicResult(httpContext);
            }

            string[] authParts = auth.Split(' ');
            if (authParts.Length != 2)
            {
                return BasicResult(httpContext);
            }

            string base64 = authParts[1];
            string authValue;
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                authValue = Encoding.ASCII.GetString(bytes);
            }
            catch (Exception e)
            {
                authValue = null;
            }

            if (string.IsNullOrEmpty(authValue))
            {
                return BasicResult(httpContext);
            }

            //解析用户名和密码
            string userName, password;
            int sepIndex = authValue.IndexOf(':');
            if (sepIndex == -1)
            {
                userName = authValue;
                password = string.Empty;
            }
            else
            {
                userName = authValue.Substring(0, sepIndex);
                password = authValue.Substring(sepIndex + 1);

            }
            //判断用户名和密码
            if (_user.UserName.Equals(userName) && _user.Password.Equals(password))
            {
                return _next(httpContext);
            }
            else
            {
                return BasicResult(httpContext);
            }



        }

        private static Task BasicResult(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Add(WWWAuthorizationHeather, "Basic realm=\"localhost\"");
            return Task.FromResult(httpContext);
        }
    }

    public class BasicUser
    {
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
