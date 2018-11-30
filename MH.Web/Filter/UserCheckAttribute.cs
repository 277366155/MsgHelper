using MH.Common;
using MH.Context;
using MH.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using MH.WxApi;

namespace MH.Web.Filter
{
    public class UserCheckAttribute : Attribute,IActionFilter
    {
        private static WxUsersContext WxUsers=>new WxUsersContext();
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
            //home允许无身份访问
            if (!context.HttpContext.IsFromWx())
            {
                return;
            }

            var code=context.HttpContext.Request.Query["code"].ToString();
            //如果请求参数有code，则从wx获取用户身份
            if (!code.IsNullOrWhiteSpace())
            {
                //获取access_token、userinfo等
                var webToken = context.HttpContext.GetWebToken(code);
                if (webToken != null && !string.IsNullOrWhiteSpace(webToken.Openid))
                {
                    //context.HttpContext.SetCookie(webToken.Openid);
                    BaseCore.CurrentContext.SetCookie(webToken.Openid);
                    WxUsers.GetWxUserInfoAndInsertToDb(webToken.Openid);
                    #region 获取当前用户信息，并缓存
                    var userInfo = new UserContext().GetUserDTOByOpenid(webToken.Openid);                    
                    var ip = context.HttpContext.Connection.RemoteIpAddress;
                    var port = context.HttpContext.Connection.RemotePort;
                    //todo:考虑在缓存时，对key添加上ip/mac地址标识。
                    CacheTools.SetData(webToken.Openid+ip+port, userInfo);
                    #endregion
                }
            }


            //若不是，取身份信息
            var userOpenid = BaseCore.CurrentContext.GetCookie();
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
