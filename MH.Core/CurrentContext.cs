using Microsoft.AspNetCore.Http;
using System;

namespace MH.Core
{
    public static class Current
    {
        public static IServiceProvider ServiceProvider { get; set; }
        static Current() { }
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
    }
}
