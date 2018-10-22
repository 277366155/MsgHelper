using System;
using System.ComponentModel.DataAnnotations;

namespace MH.Models.DBModel
{
    public  class WxUserMessage:BaseModel
    {
        /// <summary>
        /// 发送人openid
        /// </summary>
        [MaxLength(128)]
        public string FromUserName { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        [MaxLength(128)]
        public string ToUserName { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgTypeEnum MsgType { get; set; }
        /// <summary>
        /// 消息原内容
        /// </summary>
        [MaxLength(2000)]
        public string MsgContent { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long CreateTimeSpan { get; set; }
    }

    public enum MsgTypeEnum
    {
        /// <summary>
        /// 文本
        /// </summary>
        TEXT = 0,
        /// <summary>
        /// 图片
        /// </summary>
        IMAGE,
        /// <summary>
        /// 语音
        /// </summary>
        VOICE,
        /// <summary>
        /// 视频
        /// </summary>
        VIDEO,
        /// <summary>
        /// 小视频
        /// </summary>
        SHORTVIDEO,
        /// <summary>
        /// 地理位置
        /// </summary>
        LOCATION,
        /// <summary>
        /// 链接
        /// </summary>
        LINK,
        /// <summary>
        /// 事件
        /// </summary>
        EVENT
    }
}
