using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace MH.Common
{
    public static class Tools
    {
        #region 模拟请求
        /// <summary>
        /// 请求,自动根据get/post判断
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Request(RequstParam param)
        {
            if (param == null || string.IsNullOrWhiteSpace(param.Method))
            {
                return "";
            }
            switch (param.Method.ToLower())
            {
                case "get":
                    return GetRequest(param);
                case "post":
                    return PostRequest(param);
                default:
                    return "";
            }

        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string PostRequest(RequstParam param, string certpath = "", string certpwd = "")
        {
            var resultStr = "";
            //创建一个http请求
            var request = (HttpWebRequest)WebRequest.Create(param.Url);
            //request.Proxy = new WebProxy("http://127.0.0.1:8888", false);

            //设置请求类型
            request.Method = param.Method;
            //请求内容格式
            request.ContentType = param.ContentType;
            //request.Accept = "*/*";
            //request.AllowAutoRedirect = false;
            if (param.Url.Trim().ToLower().IndexOf("https") == 0)
            {
                //当请求为https时，验证服务器证书
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((a, b, c, d) => true);
                if (!string.IsNullOrEmpty(certpath) && !string.IsNullOrEmpty(certpwd))
                {
                    X509Certificate2 cer = new X509Certificate2(certpath, certpwd,
                        X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                    request.ClientCertificates.Add(cer);
                }
            }
            //获取http请求的流/管道
            var requestStream = request.GetRequestStream();
            var requestData = param.RequestData;

            //创建一个写流数据的对象
            using (var tw = new StreamWriter(requestStream))
            {
                //向流中写数据
                tw.Write(requestData);
            }
            //发起请求获取响应结果
            var rs = (HttpWebResponse)request.GetResponse();

            //获取响应结果的流/管道
            using (var getStream = rs.GetResponseStream())
            {
                using (var rd = new StreamReader(getStream, Encoding.GetEncoding(param.Encode)))
                {
                    // 读取返回结果流信息
                    resultStr = rd.ReadToEnd();
                }
            }

            return resultStr;
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetRequest(RequstParam param)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(param.Url + (string.IsNullOrWhiteSpace(param.RequestData) ? "" : "?" + param.RequestData));
            request.Method = param.Method;
            request.ContentType = param.ContentType;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(param.Encode));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// 文件post上传功能
        /// </summary>
        /// <param name="param"></param>
        /// <param name="certpath"></param>
        /// <param name="certpwd"></param>
        /// <returns></returns>
        public static string PostUpload(UploadRequestParam param, string certpath = "", string certpwd = "")
        {
            var resultStr = "";
            //创建一个http请求
            var request = (HttpWebRequest)WebRequest.Create(param.Url);
            //request.Proxy = new WebProxy("http://127.0.0.1:8888", false);

            //设置请求类型
            request.Method = param.Method;

            //request.Accept = "*/*";
            //request.AllowAutoRedirect = false;
            if (param.Url.Trim().ToLower().IndexOf("https") == 0)
            {
                //当请求为https时，验证服务器证书
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((a, b, c, d) => true);
                if (!string.IsNullOrEmpty(certpath) && !string.IsNullOrEmpty(certpwd))
                {
                    X509Certificate2 cer = new X509Certificate2(certpath, certpwd,
                        X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                    request.ClientCertificates.Add(cer);
                }
            }

            //var requestData = param.RequestData;
            var boundary = $"----{DateTime.Now.Ticks.ToString("x")}";
            var formData = $"\r\n--{boundary}\r\nContent-Disposition:form-data;name=\"{param.TypeName}\";filename=\"{param.FileName}\"\r\nContent-Type:{param.ContentType}\r\n\r\n";
            var formDataBytes = Encoding.ASCII.GetBytes(param.InputStream.Length == 0 ? formData.Substring(2, formData.Length - 2) : formData);
            //创建一个写流数据的对象
            var tw = new MemoryStream();

            //向流中写入文件头
            tw.Write(formDataBytes, 0, formDataBytes.Length);

            //向流中写数据
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = param.InputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                tw.Write(buffer, 0, bytesRead);
            }
            var footer = Encoding.ASCII.GetBytes($"\r\n--{boundary}--\r\n");
            tw.Write(footer, 0, footer.Length);
            //请求内容格式
            request.ContentType = $"multipart/form-data; boundary={boundary}";
            request.ContentLength = tw != null ? tw.Length : 0;

            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            if (tw != null)
            {
                tw.Position = 0;

                var requestStream = request.GetRequestStream();
                buffer = new byte[1024];
                bytesRead = 0;
                while ((bytesRead = tw.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                tw.Close();
            }

            //发起请求获取响应结果
            var rs = (HttpWebResponse)request.GetResponse();

            //获取响应结果的流/管道
            using (var getStream = rs.GetResponseStream())
            {
                using (var rd = new StreamReader(getStream, Encoding.GetEncoding(param.Encode)))
                {
                    // 读取返回结果流信息
                    resultStr = rd.ReadToEnd();
                }
            }

            return resultStr;
        }
        #endregion

        /// <summary>
        /// hash1加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        #region 接收post请求中的xml数据
        /// <summary>
        /// 获取post过来的xml数据
        /// </summary>
        public static string GetXMLData(IHttpContextAccessor accessor)
        {
            try
            {
                Stream inputstream = accessor.HttpContext.Request.Body; //  HttpContext.Request.InputStream;
                byte[] b = new byte[inputstream.Length];
                inputstream.Read(b, 0, (int)inputstream.Length);
                string inputstr = UTF8Encoding.UTF8.GetString(b);
                return inputstr;
            }
            catch
            {
                return "";
            }
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
        #endregion


        #region cookie操作
        private const string CookieKey = "UserID";

        /// <summary>
        /// 从cookie中拿到信息并解密
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetCookie(this IHttpContextAccessor accessor, string cookieKey = CookieKey)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
            {
                return "";
            }
            return DEncrypt.Decrypt(accessor.HttpContext.Request.Cookies[cookieKey]);
        }

        /// <summary>
        /// 从cookie中拿到信息并解密
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static string GetCookie(this FilterContext filter, string cookieKey = CookieKey)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
            {
                return "";
            }
            return DEncrypt.Decrypt(filter.HttpContext.Request.Cookies[cookieKey]);
        }


        /// <summary>
        /// 将信息加密保存到cookie
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="cookieValue"></param>
        /// <returns></returns>
        public static bool SetCookie(this IHttpContextAccessor accessor, string cookieValue, string cookieKey = CookieKey)
        {
            if (string.IsNullOrWhiteSpace(cookieValue) || string.IsNullOrWhiteSpace(cookieKey))
            {
                return false;
            }
            accessor.HttpContext.Response.Cookies.Append(CookieKey, DEncrypt.Encrypt(cookieValue), new CookieOptions() { Expires = DateTimeOffset.Now.AddHours(1) });

            return true;
        }

        #endregion
    }
}