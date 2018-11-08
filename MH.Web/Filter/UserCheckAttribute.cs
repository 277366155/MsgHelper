using MH.Common;
using MH.Context;
using MH.Models.DBModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net;

namespace MH.Web.Filter
{
    public class UserCheckAttribute : Attribute,IActionFilter
    {
        private static WxUsersContext WxUsers=>new WxUsersContext();
        public UserCheckAttribute()
        {
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 检查当前用户是否已经存在于数据库
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //用户第一次请求该域名，Cookie为空，跳转至wx端重定向之后，cookie还是空。。
            ////判断当前是否是根目录，根目录无需获取用户cookie
            //if (string.IsNullOrWhiteSpace(context.HttpContext.Request.Path.Value.Trim('/')))
            //{
            //    return;
            //}
            var code=context.HttpContext.Request.Query["code"].ToString();
            //如果请求参数有code，则从wx获取用户身份
            if (!code.IsNullOrWhiteSpace())
            {
                //获取access_token、userinfo等
                var webToken = context.HttpContext.GetWebToken(code);
                if (webToken != null && !string.IsNullOrWhiteSpace(webToken.Openid))
                {
                    context.HttpContext.SetCookie(webToken.Openid);

                    WxUsers.GetWxUserInfoAndInsertToDb(webToken.Openid);
                }
            }


            //若不是，取身份信息
            var userOpenid = context.GetCookie();
            if (string.IsNullOrWhiteSpace(userOpenid))
            {
                //无身份信息跳回主页
                //context.HttpContext.Response.Redirect();
                if (!string.IsNullOrWhiteSpace(context.HttpContext.Request.Path.Value.Trim('/')))
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.RedirectResult($"/?redirectUrl=" + WebUtility.UrlEncode(context.HttpContext.Request.Path));
                }
                return;
            }
        }        
    }
}
