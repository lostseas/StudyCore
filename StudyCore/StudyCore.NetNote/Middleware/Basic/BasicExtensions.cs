using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace StudyCore.NetNote.Middleware.Basic
{
    public static class BasicExtensions
    {
        public static IApplicationBuilder UseBasicMiddleware(this IApplicationBuilder builder, BasicUser user)
        {
            if (user == null)
            {
                throw  new ArgumentException("需设置Bsaic用户");
            }

            return builder.UseMiddleware<BasicMiddleware>(user);
        }
    }
}
