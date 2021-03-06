﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace StudyCore.Web.Middleware.RequestIP
{
    public static class RequestIPExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}
