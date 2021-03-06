﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MH.Web.Models;
using Microsoft.AspNetCore.Http;
using MH.Common;
using System.Net;
using MH.Context;
using MH.Web.Filter;
using MH.WxApi;

namespace MH.Web.Controllers
{
    [UserCheck]
    public class HomeController : BaseController
    {
        private WxUsersContext wxUsersContext => new WxUsersContext();
        public HomeController() : base()
        {
        }

        public IActionResult Index(string code, string redirectUrl)
        {
            if (!IsFromWx)
            {
                return View();
            }

            #region wx浏览器打开才执行以下方法
            //首次客户端请求不会带code参数。
            if (string.IsNullOrWhiteSpace(code))
            {
                //通过微信服务端跳回到本页面时，会在请求地址上加上code参数
                return Redirect(HttpContext.GetWebCodeRedirect());
            }

            ViewBag.Code = code;

            //若当前页面带有重定向参数，则重定向url
            if (!string.IsNullOrWhiteSpace(redirectUrl))
            {
                return Redirect(WebUtility.UrlDecode(redirectUrl));
            }
            return View();
            #endregion
        }


        public IActionResult Info()
        {
            ViewBag.Info = UserOpenid;
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
