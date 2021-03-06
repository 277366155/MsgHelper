﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MH.Web.Filter;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MH.Common.AutoMapping;

namespace MH.Web
{
    public class Startup
    {
        public static ILoggerRepository log { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //加载log4net日志配置文件
            log = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(log, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig.Configure();
            //services.AddMemoryCache();
            services.AddCors().AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                //options.Filters.Add(typeof(UserCheckFilter));
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            Core.BaseCore.ServiceProvider = svp;

            //if (env.IsDevelopment())
            //{
            //    app.UseBrowserLink();
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                //area路由注册要放在第一位，否则<a>中的asp-area属性解析会当做参数处理
                routes.MapRoute(
                              name: "areas",
                              template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
