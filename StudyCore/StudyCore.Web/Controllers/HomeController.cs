using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using StudyCore.Web.Models;

namespace StudyCore.Web.Controllers
{
    public class HomeController : Controller
    {

        private IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public void MemoryCache()
        {
            string cacheKey = "key";
            string cacheresult;
            if (!_memoryCache.TryGetValue(cacheKey, out cacheresult))
            {
                cacheresult = $"LineZero{DateTime.Now}";
                _memoryCache.Set(cacheKey, cacheresult);
                //设置相对过去时间2min
                _memoryCache.Set(cacheKey, cacheresult,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(2)));
                //设置绝对过期时间2min
                _memoryCache.Set(cacheKey, cacheresult,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                //移除缓存
                _memoryCache.Remove(cacheKey);
                //设置优先级
                _memoryCache.Set(cacheKey, cacheresult,
                    new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
                //缓存回调，10s过期回调
                _memoryCache.Set(cacheKey, cacheresult,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                        .RegisterPostEvictionCallback((key, value, reason, substate) =>
                        {
                            Console.WriteLine($"{key}的{value}被改变，应为{reason}");
                        }));
                //缓存回调，在token过期时
                var cts = new CancellationTokenSource();
                _memoryCache.Set(cacheKey, cacheresult,
                    new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(cts.Token))
                        .RegisterPostEvictionCallback((key, value, reason, subState) =>
                        {
                            Console.WriteLine($"{key}的{value}被改变，应为{reason}");
                        }));
                cts.Cancel();
            }
        }


    }
}
