using MH.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MH.Core;
using MH.Web.Filter;
using MH.Models.DTO;
using MH.WxApi;

namespace MH.Web
{
    public class BaseController: Controller
    {
        protected new static HttpContext HttpContext;
        protected string UserOpenid = "";
        protected bool IsFromWx = false;
        protected UserDTO UserInfo = null;
        public BaseController()
        {
            HttpContext = BaseCore.CurrentAccessor.HttpContext;
            IsFromWx = HttpContext.IsFromWx();
            UserOpenid = HttpContext.GetCookie();
        }
    }
}
