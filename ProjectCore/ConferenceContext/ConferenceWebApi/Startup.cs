using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Conference.EntityFrameworkCore;
using ConferenceWebApi.Filter;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Exceptionless;

namespace ConferenceWebApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 日志Repository
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        /// <summary>
        /// Startup 构造
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                //.AddJsonFile("autofac.json")//读取autofac.json文件
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// IConfigurationRoot
        /// </summary>
        public IConfigurationRoot Configuration { get; }
        
        /// <summary>
        /// 注入的容器
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// 运行时调用此方法,使用此方法将服务添加到容器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {           
            //这里就是填写数据库的链接字符串          
            var connection = Configuration.GetSection("ConnectionService")["ConnectionSqlService"];
            services.AddDbContext<ConferenceContext>(options => options.UseSqlServer(connection));                     
            return services.Configure(connection, this.Configuration);
        }

        /// <summary>
        /// //运行时调用此方法,使用此方法配置HTTP请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="appLifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
      
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();

            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            });

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            //ExceptionLess日志
            ExceptionlessClient.Default.Configuration.ApiKey = Configuration.GetSection("ExceptionLess:ApiKey").Value;
            ExceptionlessClient.Default.Configuration.ServerUrl = Configuration.GetSection("ExceptionLess:ServerUrl").Value;

            app.UseExceptionless();

            app.UseMvc();
           
            //释放注入的容器
            appLifetime.ApplicationStopped.Register(() => this.Container.Dispose());

        }
    }
}
