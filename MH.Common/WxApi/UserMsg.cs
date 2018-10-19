using Microsoft.AspNetCore.Http;
using System.Xml;

namespace MH.Common
{
    public partial  class WxApi
    {
        #region 用户消息
        /// <summary>
        /// 获取用户发送的信息。用户发送公众号信息之后，公众号配置接口会被post用户发送的信息
        /// 文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140453
        /// </summary>
        /// <returns></returns>
        public static WXMsgBase GetUserMsg(this IHttpContextAccessor accessor)
        {
            var data = Tools.GetXMLData(accessor.HttpContext);            
            return XmlToObj(data);
        }

        public static WXMsgBase XmlToObj(string xmlStr)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            var msgType = xmlDoc.GetElementsByTagName("MsgType").Item(0).InnerText;
            WXMsgBase result = null;

            switch (msgType)
            {
                case MsgType.Text:
                    result = xmlStr.XMLToModel<WXTextMsg>();
                    break;
                case MsgType.Img:
                    result = xmlStr.XMLToModel<WXImgMsg>();
                    break;
                case MsgType.Link:
                    result = xmlStr.XMLToModel<WXLinkMsg>();
                    break;
                case MsgType.Location:
                    result = xmlStr.XMLToModel<WXLocationMsg>();
                    break;
                case MsgType.Video:
                    result = xmlStr.XMLToModel<WXVideoMsg>();
                    break;
                case MsgType.ShortVideo:
                    result = xmlStr.XMLToModel<WXShortVideoMsg>();
                    break;
                case MsgType.Voice:
                    result = xmlStr.XMLToModel<WXVoiceMsg>();
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
