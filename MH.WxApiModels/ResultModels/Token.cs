namespace MH.WxApiModels
{
    #region token信息
    /// <summary>
    /// 获取的token信息
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        /// 过期时间,秒
        /// </summary>
        public long Expires_in { get; set; }
    }

    /// <summary>
    /// 第三方网站授权token信息
    /// </summary>
    public class WebToken : AccessToken
    {
        /// <summary>
        /// 刷新token
        /// </summary>
        public string Refresh_token { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// scope类型
        /// </summary>
        public string Scope { get; set; }
    }
    #endregion

}
