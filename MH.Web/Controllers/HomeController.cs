using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MH.Web.Models;
using Microsoft.AspNetCore.Http;
using MH.Common;
using System.Net;
using Newtonsoft.Json;
using MH.Models.DBModel;
using Microsoft.EntityFrameworkCore;
using MH.Context;
using MH.Web;
using MH.Web.Filter;

namespace MH.Web.Controllers
{
    [UserCheck]
    public class HomeController : BaseController
    {
        private WxUsersContext wxUsersContext;
        public HomeController(IHttpContextAccessor _accessor) : base(_accessor)
        {
            wxUsersContext = new WxUsersContext();
        }

        public IActionResult Index(string code,string redirectUrl)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Redirect(accessor.GetWebCodeRedirect());
            }

            ViewBag.Code = code;
            //获取access_token、userinfo等
            var webToken = accessor.GetWebToken(code);
            if (webToken != null&&!string.IsNullOrWhiteSpace(webToken.Openid))
            {
                accessor.SetCookie(webToken.Openid);                
            }
            if (!string.IsNullOrWhiteSpace(redirectUrl))
            {
                return Redirect(WebUtility.UrlDecode(redirectUrl));
            }
            return View();
        }


        public IActionResult Info()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
