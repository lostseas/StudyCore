using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StudyCore.Web.Config.Autofac;
using StudyCore.Web.Middleware.RequestIP;

namespace StudyCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //添加memoryCaching
            services.AddMemoryCache();
            //添加session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);//10s的过期时间
            });

            services.AddMvc();
            //启用目录浏览
            services.AddDirectoryBrowser();

            //添加Autofac第三方容器  返回类型void改为IServiceProvider
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaulModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            #region 默认页
            //app.UseDefaultFiles();//添加默认首页
            ////修改默认首页
            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("default.html");
            //app.UseDefaultFiles(options);//添加默认首页 
            #endregion

            //配置使用session 必须添加在MVC之前
            app.UseSession();

            app.UseStaticFiles();

            ////使用wwwroot外部的静态文件
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider =
            //        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"myStaticFiles")),
            //    RequestPath = new PathString("/StaticFiles")//文件路径
            //});
            //启用目录浏览
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider =
                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"myStaticFiles")),
                RequestPath = new PathString("/StaticFiles") //文件路径
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UserRequestIP();//每次请求是都会记录用户请求IP地址
        }
    }
}
