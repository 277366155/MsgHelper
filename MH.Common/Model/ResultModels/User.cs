using System.Collections.Generic;

namespace MH.Common
{
    #region 用户列表信息
    public class OpenidListModel
    {
        public int Total { get; set; }

        public int Count { get; set; }

        public OpenidModel Data { get; set; }

        public string Next_Openid { get; set; }
    }

    public class OpenidModel
    {
        public List<string> Openid { get; set; }
    }
    #endregion

    #region 用户详细信息
    public class UserInfoList
    {
        public List<UserInfo> User_info_list { get; set; }
    }

    public class UserInfo
    {
        public int Subscribe { get; set; }
        public string Openid { get; set; }
        public string NickName { get; set; }
        public int Sex { get; set; }
        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string HeadImgUrl { get; set; }
        public long subscribe_time { get; set; }
        public string Remark { get; set; }
        public int GroupId { get; set; }
        public string Subscribe_scene { get; set; }
    }
    #endregion
}
