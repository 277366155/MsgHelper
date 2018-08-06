using System;
using System.Collections.Generic;
using System.Text;

namespace MH.Common
{
    public partial class WxApi
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
