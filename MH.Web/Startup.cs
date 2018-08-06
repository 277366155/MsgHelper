using MH.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MH.Web.Filter;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;

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
            services.AddMemoryCache();
            services.AddCors().AddMvc(options=> {
                options.Filters.Add(typeof(ExceptionFilter));
                //options.Filters.Add(typeof(UserCheckFilter));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddDbContext<MHContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySqlConnection")));
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

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
