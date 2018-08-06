using MH.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MH.Web
{
    public class BaseController: Controller
    {
        protected static IHttpContextAccessor accessor;
        protected string UserOpenid = "";
        public BaseController(IHttpContextAccessor _accessor)
        {
            accessor = _accessor;
            UserOpenid=accessor.GetCookie();
        }
    }
}
