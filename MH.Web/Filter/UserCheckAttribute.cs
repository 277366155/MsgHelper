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

            //判断当前是否是根目录，根目录无需获取用户cookie
            if (string.IsNullOrWhiteSpace(context.HttpContext.Request.Path.Value.Trim('/')))
            {
                return;
            }

            //若不是，取身份信息
            var userOpenid = context.GetCookie();
            if (string.IsNullOrWhiteSpace(userOpenid))
            {
                //无身份信息跳回主页
                //context.HttpContext.Response.Redirect();
                context.Result =new Microsoft.AspNetCore.Mvc.RedirectResult($"/?redirectUrl=" + WebUtility.UrlEncode(context.HttpContext.Request.Path));
                return;
            }
 
            WxUsers.GetWxUserInfoAndInsertToDb(userOpenid);
        }        
    }
}
