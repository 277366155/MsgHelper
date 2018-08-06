using log4net;
using MH.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MH.Web.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private ILog log = LogManager.GetLogger(Startup.log.Name,typeof(ExceptionFilter));
        //private MHContext mhContext;
        //public ExceptionFilter(DbContextOptions options)
        //{
        //    mhContext = new MHContext(options);
        //}
        public void OnException(ExceptionContext context)
        {
            log.Error(context.Exception);
        }
    }
}
