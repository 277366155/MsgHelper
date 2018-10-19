using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MH.Common
{
    public static partial class WxApi
    {
        public static string WXCreateMenuUrl = Configuration["AppSettings:WxConfig:CreateMenuUrl"];//https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN
        public static string WXDeleteMenuUrl = Configuration["AppSettings:WxConfig:DeleteMenuUrl"];//https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        public static string WXSelectMenuUrl = Configuration["AppSettings:WxConfig:SelectMenuUrl"];//https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
        /// <summary>
        /// 创建自定义菜单
        /// 文档地址:https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141013
        /// 请求地址:https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN
        /// </summary>
        /// <param name="buttonParam"></param>
        public static string CreateMenu(ButtonParam buttonParam)
        {
            var requestUrl = WXCreateMenuUrl + "?access_token=" + AccessToken;
            var result = Tools.PostRequest(new PostParam()
            {
                ContentType = ContentType.FormUrlEncoded,
                Url = requestUrl,
                RequestData = buttonParam.ObjToJson()
            });
            return result;
        }

        /// <summary>
        /// 删除默认菜单和全部个性化
        /// http请求方式：GET https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string DeleteMenu()
        {
            var requestUrl = WXDeleteMenuUrl + "?access_token=" + AccessToken;
            var result = Tools.GetRequest(new GetParam() {  ContentType = ContentType.TextHtml, Url = requestUrl });
            return result;
        }

        /// <summary>
        /// 查询所有菜单信息
        /// http请求方式：GET https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string SelectMenu()
        {
            var requestUrl = WXSelectMenuUrl + "?access_token=" + AccessToken;
            var result = Tools.GetRequest(new GetParam() {  ContentType = ContentType.TextHtml, Url = requestUrl });
            return result;
        }

    }
}
