using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace StudyCore.Web.Config.Autofac
{
    /// <summary>
    /// 默认模块
    /// </summary>
    public class DefaulModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            //要注入的服务
            //builder.RegisterType<ServiceProvider>().As<IServiceProvider>();
        }
    }
}
