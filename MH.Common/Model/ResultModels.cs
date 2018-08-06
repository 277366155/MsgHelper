using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MH.Common
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

    #region 用户列表信息
    public class UserListModel
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

    #region 接收消息model
    /// <summary>
    /// 微信消息类型
    /// </summary>
    public class MsgType
    {
        /// <summary>
        /// 文本
        /// </summary>
        public const string Text = "text";
        /// <summary>
        /// 图片
        /// </summary>
        public const string Img = "image";
        /// <summary>
        /// 语音
        /// </summary>
        public const string Voice = "voice";
        /// <summary>
        /// 视频
        /// </summary>
        public const string Video = "video";
        /// <summary>
        /// 小视频
        /// </summary>
        public const string ShortVideo = "shortvideo";
        /// <summary>
        /// 地理位置
        /// </summary>
        public const string Location = "location";
        /// <summary>
        /// 链接
        /// </summary>
        public const string Link = "link";
        /// <summary>
        /// 事件
        /// </summary>
        public const string Event = "event";
    }


    /// <summary>
    /// 接收微信xml数据基类
    /// </summary>
    [XmlRoot("xml")]
    public class WXXmlDataBase
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方openid
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 消息类型：文本-text 图片-image 语音-voice 视频-video 小视频-shortvideo 地理位置-location 链接-link
        /// </summary>
        public string MsgType { get; set; }
    }

    #region 事件
    /// <summary>
    /// 
    /// </summary>
    public class WXEventBase : WXXmlDataBase
    {
        public string Event { get; set; }
    }

    #endregion

    #region 消息接收

    /// <summary>
    /// 接收微信消息基类
    /// </summary>
    public class WXMsgBase : WXXmlDataBase
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public long MsgId { get; set; }
    }

    /// <summary>
    /// 多媒体消息基类
    /// </summary>
    [XmlRoot("xml")]
    public class WXMediaMsgBase : WXMsgBase
    {
        /// <summary>
        /// 多媒体消息id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXTextMsg : WXMsgBase
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXImgMsg : WXMediaMsgBase
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

    }

    /// <summary>
    /// 语音消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXVoiceMsg : WXMediaMsgBase
    {
        /// <summary>
        /// 语音格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXVideoMsg : WXMediaMsgBase
    {
        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 小视频消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXShortVideoMsg : WXVideoMsg
    {

    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXLocationMsg : WXMsgBase
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Location_X { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public decimal Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    [XmlRoot("xml")]
    public class WXLinkMsg : WXMsgBase
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
    #endregion

    #endregion


}
