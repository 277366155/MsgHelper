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
        public ContentType ContentType { get; set; }
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

	public abstract class ContentType
	{
		public abstract string Value { get; }
	}

	public class FormUrlEncoded : ContentType
	{
		public override string Value   => "application/x-www-form-urlencoded"; 
	}

	public class ApplicationJson : ContentType
	{
		public override string Value => "application/json; charset=utf-8";
	}

	public class OctetStream : ContentType
	{
		public override string Value => "application/octet-stream";
	}

	public class TextHtml : ContentType
	{
		public override string Value => "text/html;charset=UTF-8";
	}


	public class FormData : ContentType
	{
		public override string Value => "multipart/form-data";
	}
}
