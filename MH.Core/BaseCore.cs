using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MH.Core
{
    public static class BaseCore
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IHttpContextAccessor CurrentAccessor
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));
                return (IHttpContextAccessor)factory;
            }
        }
        public static HttpContext CurrentContext
        {
            get
            {
                return CurrentAccessor.HttpContext;
            }
        }


        /// <summary>
        /// 读取配置
        /// </summary>
        public static IConfigurationRoot Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                return builder.Build();
            }
        }
    }
}
