using MH.Common;

namespace MH.WxApi
{
    public static partial class WxApi
    {
        #region 图片类型素材
        public static string UploadImg(UploadRequestParam requst)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token="+AccessToken;
           return Tools.PostUpload(requst);
        }
        #endregion
    }
}
