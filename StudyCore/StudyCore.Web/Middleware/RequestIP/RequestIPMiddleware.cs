using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;

namespace StudyCore.Web.Middleware.RequestIP
{
    /// <summary>
    /// 获得IP信息中间件
    /// </summary>
    public class RequestIPMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger _logger;

        public RequestIPMiddleware(RequestDelegate next, ILogger logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("user IP:"+context.Connection.RemoteIpAddress.ToString());
            await _next.Invoke(context);
        }
    }
}
