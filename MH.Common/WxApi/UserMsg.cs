using Microsoft.AspNetCore.Http;
using System.Xml;

namespace MH.Common
{
    public partial  class WxApi
    {
        #region 用户消息
        /// <summary>
        /// 获取用户发送的信息
        /// 文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140453
        /// </summary>
        /// <returns></returns>
        public static WXMsgBase GetUserMsg(this IHttpContextAccessor accessor)
        {
            var data = Tools.GetXMLData(accessor);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);
            var msgType = xmlDoc.GetElementsByTagName("MsgType").Item(0).InnerText;
            WXMsgBase result = null;

            switch (msgType)
            {
                case MsgType.Text:
                    result = data.XMLToModel<WXTextMsg>();
                    break;
                case MsgType.Img:
                    result = data.XMLToModel<WXImgMsg>();
                    break;
                case MsgType.Link:
                    result = data.XMLToModel<WXLinkMsg>();
                    break;
                case MsgType.Location:
                    result = data.XMLToModel<WXLocationMsg>();
                    break;
                case MsgType.Video:
                    result = data.XMLToModel<WXVideoMsg>();
                    break;
                case MsgType.ShortVideo:
                    result = data.XMLToModel<WXShortVideoMsg>();
                    break;
                case MsgType.Voice:
                    result = data.XMLToModel<WXVoiceMsg>();
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        #endregion
    }
}
