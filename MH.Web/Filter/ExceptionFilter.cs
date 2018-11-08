using log4net;
using MH.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MH.Web.Filter
{
    //IFilterMetadata主要起到标记的作用
    public class ExceptionFilter : IExceptionFilter, IFilterMetadata
    {
        private ILog log = LogManager.GetLogger(Startup.log.Name, typeof(ExceptionFilter));
        //private MHContext mhContext;
        //public ExceptionFilter(DbContextOptions options)
        //{
        //    mhContext = new MHContext(options);
        //}
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                log.Error(context.Exception);

                context.Result = new ContentResult() {   StatusCode=200, Content=$"<script>alert('{context.Exception.Message}');</script>" } ;
                
                context.ExceptionHandled = true;
            }
        }
    }

    internal class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object obj) : base(obj)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

    }
}
