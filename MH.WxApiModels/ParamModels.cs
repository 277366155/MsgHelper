﻿using System.Collections.Generic;
using System.IO;

namespace MH.WxApiModels
{
    
    public class OpenidListParam
    {
        public List<GetUserInfoParam> user_list { get; set; }
    }

    public class GetUserInfoParam
    {
        public string openid { get; set; }

        public string lang { get; set; }
    }

    #region 自定义菜单
    public enum ButtonType
    {
        //key值
        click = 1,
        //跳转url
        view,
        //向后端传送扫描二维码结果
        scancode_push,
        //扫描二维码并返回结果给微信端
        scancode_waitmsg,
        //拍照并发送到后端，后端可处理返回
        pic_sysphoto,
        //拍照或从相册选择
        pic_photo_or_album,
        //微信相册选择后发送到后端，后端可处理返回
        pic_weixin,
        //选择地理位置发送到后端，后端可处理返回
        location_select,
        //调取小程序
        miniprogram
    }
    public class BaseButton
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name
        {
            get; set;
        }
    }

    /// <summary>
    /// 一级菜单
    /// </summary>
    public class Button : BaseButton
    {
        public string type { get; set; }
    }
    /// <summary>
    /// click类型的菜单按钮
    /// </summary>
    public class ClickButton : Button
    {
        public string key { get; set; }
    }

    /// <summary>
    /// View类型菜单
    /// </summary>
    public class ViewButton : Button
    {
        public string url { get; set; }
    }

    /// <summary>
    /// 小程序类型的菜单
    /// </summary>
    public class MiniProgramButton : Button
    {
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

    /// <summary>
    /// 二级菜单
    /// </summary>
    public class SubButtonList : BaseButton
    {
        public List<Button> sub_button { get; set; }
    }

    public class ButtonParam
    {
        public List<BaseButton> button { get; set; }
    }
    #endregion

    #region 客服模型
    public class CustomServiceParam
    {
        /// <summary>
        /// 客服账号
        /// </summary>
        public string kf_account { get; set; }

        /// <summary>
        /// 客服昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 客服密码
        /// </summary>
        public string password { get; set; }
    }
    #endregion
}