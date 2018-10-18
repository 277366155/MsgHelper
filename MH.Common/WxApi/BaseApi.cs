﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MH.Common
{
    public static partial class WxApi
    {


        /// <summary>
        /// 读取配置
        /// </summary>
        private static IConfigurationRoot Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                return builder.Build();
            }
        }
        public static string APPID = Configuration["AppSettings:WxConfig:AppId"];
        public static string Secret = Configuration["AppSettings:WxConfig:Secret"];
        public static string WXTokenUrl = Configuration["AppSettings:WxConfig:TokenUrl"]; //"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
        public static string WXWebAuthoUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={appid}&redirect_uri={url}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
        public static string WxWebTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={appid}&secret={secret}&code={code}&grant_type=authorization_code";
        public const string AccessTokenKey = "WXAccessToken";
        public const string WebTokenKey = "WXWebToken";
        #region 检测是否来自微信的请求
        /// <summary>
        /// 检测请求是否来自微信
        /// </summary>
        /// <returns>来自wx则原样返回echostr,否则返回null</returns>
        public static string CheckWX(this IHttpContextAccessor accessor)
        {
            var signature = accessor.HttpContext.Request.Query["signature"];
            var timestamp = accessor.HttpContext.Request.Query["timestamp"];
            var nonce = accessor.HttpContext.Request.Query["nonce"];
            var echostr = accessor.HttpContext.Request.Query["echostr"];
            var token = "admin";

            //三个参数排序
            string[] strs = { nonce, timestamp, token };
            var strList = strs.OrderBy(m => m);
            string checkStr = "";

            foreach (var str in strList)
            {
                checkStr += str;
            }
            //拼接之后hash加密
            checkStr = Tools.SHA1(checkStr, Encoding.UTF8).ToLower();

            //WXAPI.WXAPI.GetUserMsg();

            //比较两个值，如果不等相等，则说明不是微信发来的请求
            if (checkStr != signature)
            {
                return null;
            }

            return echostr;
        }
        #endregion

        #region 获取AccessToken并缓存
        /// <summary>
        ///获取accessToken：先从缓存中取，不存在则再去weixin请求
        ///文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140183
        ///请求地址： https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx9acdab4b5113afec&secret=77bfea082d25f047dc2d7fd80f59ca8e
        /// </summary>
        /// <returns></returns>
        public static string AccessToken
        {
            get
            {
                var cache = new MemoryCache(new MemoryCacheOptions());  // MemoryCache.Default;
                object cacheValue = cache.Get(AccessTokenKey);
                //cache.TryGetValue(AccessTokenKey,out cacheValue);
                if (cacheValue != null)
                {
                    return cacheValue.ToString();
                }

                string result = "";
                string requestUrl = WXTokenUrl + "&appid=" + APPID + "&Secret=" + Secret;
                var resultStr = Tools.Request(new RequstParam() { Method = "GET", Encode = "UTF-8", Url = requestUrl, ContentType = ContentType.TextHtml, RequestData = "" });
                var resultObj = JsonConvert.DeserializeObject<AccessToken>(resultStr);
                if (resultObj != null)
                {
                    cache.Set(AccessTokenKey, resultObj.Access_token, DateTimeOffset.Now.AddSeconds(resultObj.Expires_in));
                    result = resultObj.Access_token;
                }
                return result;
            }
        }

        #endregion

        #region 网页授权

        /// <summary>
        /// web授权获取
        ///网页授权 https://open.weixin.qq.com/connect/oauth2/authorize?appid={appid}&redirect_uri={url}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect
        /// </summary>
        /// <returns></returns>
        public static string GetWebCodeRedirect(this IHttpContextAccessor accessor)
        {
            var request = accessor.HttpContext.Request;
            var currentUrl = WebUtility.UrlEncode(request.Scheme + "://" + request.Host + request.Path+request.QueryString);
            var requestUrl = WXWebAuthoUrl.Replace("{appid}", APPID).Replace("{url}", currentUrl);
            return requestUrl;
        }

        /// <summary>
        /// 网页授权token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WebToken GetWebToken(this IHttpContextAccessor accessor,string code = "")
        {
            string requestUrl = WxWebTokenUrl.Replace("{appid}", APPID).Replace("{secret}", Secret).Replace("{code}", code);
            var resultStr = Tools.Request(new RequstParam() { Method = "GET", Encode = "UTF-8", Url = requestUrl, ContentType = ContentType.TextHtml, RequestData = "" });
            var resultObj = JsonConvert.DeserializeObject<WebToken>(resultStr);

            return resultObj;
        }
        #endregion
    }
}