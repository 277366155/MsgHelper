using System.IO;

namespace MH.Common
{
    /// <summary>
    /// 模拟post/get请求参数
    /// </summary>
    public class RequstParam
    {
        public string Url { get; set; }
        /// <summary>
        /// 取ContentType类中只读字段
        /// </summary>
        public string ContentType { get; set; }
        public string Encode { get; set; }
        public MethodEnum? Method { get; set; }
        public string RequestData { get; set; }
    }

    /// <summary>
    /// 请求类型
    /// </summary>
    public enum MethodEnum
    {
        Post = 0,
        Get = 1
    }

    /// <summary>
    /// post上传文件请求参数
    /// </summary>
    public class UploadRequestParam : RequstParam
    {
        public new MethodEnum Method = MethodEnum.Post;
        public new string Encode => "UTF-8";
        public string TypeName => "media";
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
    }

    public class PostParam : RequstParam
    {
        public new MethodEnum Method => MethodEnum.Post;
        public new string Encode => "UTF-8";
    }

    public class GetParam : RequstParam
    {
        public new MethodEnum Method => MethodEnum.Get;
        public new string Encode => "UTF-8";

    }

    public static class ContentType
    {
        /// <summary>
        /// post提交application/x-www-form-urlencoded
        /// </summary>
        public readonly static string FormUrlEncoded = "application/x-www-form-urlencoded";
        /// <summary>
        /// post提交application/json
        /// </summary>
        public readonly static string Json = "application/json; charset=utf-8";

        ///// <summary>
        /// post上传文件流
        /// </summary>
        public readonly static string OctetStream = "application/octet-stream";

        /// <summary>
        /// get提交text/html;charset=UTF-8
        /// </summary>
        public readonly static string TextHtml = "text/html;charset=UTF-8";
    }
}
