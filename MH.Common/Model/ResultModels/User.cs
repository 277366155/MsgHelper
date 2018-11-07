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
        public List<MassUserInfo> User_info_list { get; set; }
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

    /// <summary>
    /// 批量获取用户详情的model
    /// </summary>
    public class MassUserInfo:UserInfo
    {
        public string UnionId { get; set; }

        public int[] TagId_List { get; set; }

        public int qr_scene { get; set; }

        public string qr_scene_str { get; set; }
    }
    #endregion
}
