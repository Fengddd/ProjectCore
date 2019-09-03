using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Conference.Common;
using Conference.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceWebApi
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// 注入的容器
        /// </summary>
        public static Autofac.IContainer Container { get; private set; }

        /// <summary>
        /// AutoFacServiceProvider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static AutofacServiceProvider Configure(this IServiceCollection services, string connectionString, IConfigurationRoot configuration)
        {        
            //注入
            DependencyInjectionService.GetInstance(services)
                .AddMvc()
                .AddCookie()
                .AddSwagger()
                .AddCors()
                .AddDddLayering()
                .AddCap();

            //AutoFac依赖注入 
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var module = new ConfigurationModule(configuration);
            builder.RegisterModule(module);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }
    }
}
