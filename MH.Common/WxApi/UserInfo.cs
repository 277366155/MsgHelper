using MH.Core;
using Newtonsoft.Json;

namespace MH.Common
{
    public partial class WxApi
    {
        public static string WXUserListUrl = BaseCore.Configuration["AppSettings:WxConfig:UserListUrl"]; //"https://api.weixin.qq.com/cgi-bin/user/get";
        public static string WXUserInfoUrl = BaseCore.Configuration["AppSettings:WxConfig:UserInfoUrl"]; //"https://api.weixin.qq.com/cgi-bin/user/info";
        public static string WXBatchUserInfoUrl = BaseCore.Configuration["AppSettings:WxConfig:BatchUserInfoUrl"]; // "https://api.weixin.qq.com/cgi-bin/user/info/batchget";
        
        #region 用户信息



        /// <summary>
        /// 获取已关注的用户openid列表信息，需要认证
        /// 文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140840
        /// 请求地址：https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid=NEXT_OPENID
        /// </summary>
        /// <param name="nextOpenid"></param>
        /// <returns></returns>
        public static string GetUserList(string nextOpenid = "")
        {
            var requestUrl = WXUserListUrl + "?access_token=" + AccessToken;
            if (!string.IsNullOrWhiteSpace(nextOpenid))
            {
                requestUrl += "&&next_openid=" + nextOpenid;
            }
            var resultStr = Tools.GetRequest(new GetParam() {  Url = requestUrl, ContentType = ContentType.TextHtml, RequestData = "" });
            return resultStr;

        }


        /// <summary>
        /// 获取用户详细信息，需要认证
        /// 文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140839
        /// 请求地址：https://api.weixin.qq.com/cgi-bin/user/info?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
        /// </summary>
        /// <param name="openId">用户openid</param>
        /// <param name="lang">语言:zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static string GetUserInfo(string openId, string lang = "zh_CN")
        {
            var requestUrl = WXUserInfoUrl + "?access_token=" + AccessToken + "&openid=" + openId + "&lang=" + lang;
            var resultStr = Tools.GetRequest(new GetParam() {  Url = requestUrl, ContentType = ContentType.TextHtml, RequestData = "" });
            return resultStr;
        }

        /// <summary>
        ///批量获取用户信息，最多获取100条数据，需要认证【调试未通过，errorCode:40032】
        ///文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140839
        ///请求地址：https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string GetBatchUserInfos(OpenidListParam param)
        {
            var requestUrl = WXBatchUserInfoUrl + "?access_token=" + AccessToken;
            if (param == null)
            {
                param = new OpenidListParam();
            }
            var resultStr = Tools.PostRequest(new PostParam() { Url = requestUrl, ContentType = ContentType.FormUrlEncoded, RequestData = param.ObjToJson() });
            return resultStr;
        }
        #endregion




    }
}
