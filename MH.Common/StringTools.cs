using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MH.Common
{
    public static class StringTools
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 将对象转为json字符串
        /// </summary>
        /// <param name="obj">对象模型</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            if (obj != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            return "";
        }

        /// <summary>
        /// json字符串转为对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public static T ToObj<T>(this string json)
        {
            if (!json.IsNullOrWhiteSpace())
            {				
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }


        /// <summary>
        /// xml转为Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T XMLToModel<T>(this string xml, Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return default(T);
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                using (StreamReader streamReader = new StreamReader(ms, encoding))
                {
                    return (T)xmlSerializer.Deserialize(streamReader);
                }
            }
        }
    }
}
