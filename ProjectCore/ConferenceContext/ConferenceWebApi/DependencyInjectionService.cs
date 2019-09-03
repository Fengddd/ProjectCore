
using System.IO;
using Conference.Command;
using Conference.CommandHandler;
using Conference.Common;
using Conference.Common.IocHelper;
using Conference.Common.Log;
using Conference.Domain;
using Conference.EntityFrameworkCore;
using Conference.QueryService;
using Conference.QueryService.EventHandler;
using ConferenceWebApi.Filter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace ConferenceWebApi
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class DependencyInjectionService
    {
        private static DependencyInjectionService _dependencyInjectionConfiguration;
        private static readonly object LockObj = new object();
        private static IServiceCollection _services;

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static DependencyInjectionService GetInstance(IServiceCollection services)
        {
            _services = services;
            if (_dependencyInjectionConfiguration == null)
            {
                lock (LockObj)
                {
                    if (_dependencyInjectionConfiguration == null)
                    {
                        _dependencyInjectionConfiguration = new DependencyInjectionService();
                    }
                }
            }
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加MVC
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddMvc()
        {
            _services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(LogHelper));
                }).
                SetCompatibilityVersion(CompatibilityVersion.Version_2_2).
                AddJsonOptions(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddCookie()
        {
            //注册Cookie认证服务
            _services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加CAP
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddCap()
        {
            _services.AddCap(x =>
            {
                // 如果你的 SqlServer 使用的 EF 进行数据操作，你需要添加如下配置： 
                x.UseEntityFramework<ConferenceContext>();
                //启用操作面板
                x.UseDashboard();
                // 如果你使用的 RabbitMQ 作为MQ，你需要添加如下配置：              
                x.UseRabbitMQ(cfg =>
                {
                    cfg.HostName = "127.0.0.1";
                    cfg.UserName = "guest";
                    cfg.Password = "guest";
                });
                //设置失败重试次数
                x.FailedRetryCount = 5;
            });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加跨域
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddCors()
        {
            //配置跨域处理
            _services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();//指定处理cookie
                });
            });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddSwagger()
        {
            //注册Swagger生成器，定义一个和多个Swagger 文档
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None"

                });
                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<HttpHeaderFilter>();
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "WebApiSwagger.xml");
                c.IncludeXmlComments(xmlPath);

            });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加DDD分层的注入
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddDddLayering()
        {
            //注入ExceptionLessLogger服务
            _services.AddSingleton<ILoggerHelper, ExceptionLessLogger>();

            //发送命令事件
            _services.AddScoped<ICommandService, CommandService>();
            //注入命令处理器（CAP的订阅注入必须在AddCap之前）
            _services.AddScoped<IConferenceCommandHandler, ConferenceCommandHandler>();

            //注入事件处理器（CAP的订阅注入必须在AddCap之前）
            _services.AddScoped<IConferenceEventHandler, ConferenceEventHandler>();
            //领域层发布事件
            _services.AddScoped<IPublishDomainEventService, PublishDomainEventService>();
            //注入泛型仓储基类
            //_services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //_services.AddScoped<IConferenceRepository, ConferenceRepository>();
            _services.AddScoped<IConferenceQueryService, ConferenceQueryService>();
            //通过反射进行依赖注入不能为空
            //注入仓储
            _services.RegisterAssembly("Conference.Domain", "Conference.EntityFrameworkCore");
            ////注入领域服务
            //_services.RegisterDomainServiceAssembly("Conference.DomainService");
            //services.RegisterAssembly("ProjectCore.Domain.DomainService");
            //Autofac依赖注入 Class的后面名字必须一致才能注入

            return _dependencyInjectionConfiguration;
        }


    }
}
