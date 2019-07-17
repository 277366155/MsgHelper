using MH.Common;
using MH.WxApiModels;

namespace MH.WxApi
{
    //客服账号管理
    public static partial class WxApi
    {
        private static string AddCustomerServiceUrl = "https://api.weixin.qq.com/customservice/kfaccount/add?access_token={token}";

        /// <summary>
        /// 添加客服账号。官方已下架该接口
        ///  post请求地址：https://api.weixin.qq.com/customservice/kfaccount/add?access_token=ACCESS_TOKEN
        ///  文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140547
        /// </summary>
        public static CustomServiceResult AddCustomerService(CustomServiceParam param)
        {
            var requestUrl = AddCustomerServiceUrl.Replace("{token}", AccessToken);
            var data = Tools.Post(new PostParam()
            {
                ContentType = new ApplicationJson(),
                Url = requestUrl,
                RequestData = param.ToJson()
            });

            return data.ToObj<CustomServiceResult>();
        }
    }
}
