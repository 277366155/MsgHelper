using System;
using System.Text;

namespace MH.ConsoleTest
{
	public static class Sms
	{
		public static void SendMsg()
		{
			ChinaSms sms = new ChinaSms("jiashimei", "jsm168");
			while (true)
			{
				Console.WriteLine("输入接收短信的手机号码(回车结束): ");
				var tel = Console.ReadLine();
				Console.WriteLine("输入发送的短信内容(回车结束): ");
				var msg = Console.ReadLine();

				Console.WriteLine($"是否给[{tel}]发送以下内容[{msg}]（yes or no）: ");
				if (Console.ReadLine().ToLower() == "yes")
				{
					var re = sms.SendSms(tel, msg);
					Console.WriteLine(re);
					Console.WriteLine("-------------------------------------------");
				}
			}
		}

		/// <summary>
		/// 简单封装中国短信接口规范V1.5.0331
		/// </summary>
		public class ChinaSms
		{
			private string comName;
			private string comPwd;

			public ChinaSms()
			{
			}

			public ChinaSms(String name, String pwd)
			{
				this.comName = name;
				this.comPwd = pwd;
			}


			/// <summary>
			/// 发送接口
			/// </summary>
			/// <param name="dst">目标手机号码，多个以英文逗号分隔，最后一个手机号后面不加分隔符。
			/// get方式最多支持100个号码，post方式最多支持500个号码</param>
			/// <param name="msg">发送短信内容</param>
			/// <returns>true:发送成功 false:发送失败</returns>
			public bool SendSms(String dst, String msg)
			{
				string sUrl = null;  //接口规范中的地址
				string sMsg = null;  //调用结果
				string encode = "GB2312"; // 支持GB2312, UTF-8
										  //内容和用户名需要进行gb2312或者utf-8编码
				msg = System.Web.HttpUtility.UrlEncode(msg, System.Text.Encoding.GetEncoding(encode));
				comName = System.Web.HttpUtility.UrlEncode(comName, System.Text.Encoding.GetEncoding(encode));

				//gb2312调用地址
				sUrl = "http://180.76.112.107/send/gsendv2.asp?name=" + comName + "&pwd=" + comPwd + "&dst=" + dst + "&msg=" + msg;
				//utf8调用地址
				//sUrl = "http://180.76.112.107/send/gsendv2_utf8.asp?name=" + comName + "&pwd=" + comPwd + "&dst=" + dst + "&msg=" + msg;

				sMsg = GetUrl(sUrl, encode);
				Console.WriteLine(sMsg);

				if (sMsg.Substring(0, 5) != "num=0")
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			/// <summary>
			/// 通用调用接口发送
			/// </summary>
			/// <param name="urlString"></param>
			/// <returns></returns>
			public String GetUrl(String urlString, string encode)
			{
				string sMsg = "";       //引用的返回字符串
				try
				{
					System.Net.HttpWebResponse rs = (System.Net.HttpWebResponse)System.Net.HttpWebRequest.Create(urlString).GetResponse();
					System.IO.StreamReader sr = new System.IO.StreamReader(rs.GetResponseStream(), System.Text.Encoding.GetEncoding(encode));
					sMsg = sr.ReadToEnd();
				}
				catch
				{
					return sMsg;
				}
				return sMsg;
			}



			/// <summary>
			/// 查询用户余额
			/// </summary>
			/// <param name="name">用户名</param>
			/// <param name="pwd">密码</param>
			/// <returns></returns>
			public String GetFee()
			{
				string sUrl = null; //请求地址
				string sMsg = null; //调用返回结果
				try
				{
					string encode = "GB2312"; //// 支持GB2312, UTF-8
											  //用户名需要进行gb2312或者utf-8编码
					comName = System.Web.HttpUtility.UrlEncode(comName, System.Text.Encoding.GetEncoding(encode));
					//gb2312调用地址
					sUrl = "http://180.76.112.107/send/getfeev2.asp?name=" + comName + "&pwd=" + comPwd;
					//utf8调用地址
					//sUrl = "http://180.76.112.107/send/getfeev2_utf8.asp?name=" + comName + "&pwd=" + comPwd ;

					sMsg = GetUrl(sUrl, encode);

					return sMsg;
					//Console.WriteLine(sMsg);
				}
				catch (Exception e)
				{
					return sMsg;
				}


			}



			/// <summary>
			/// 检查内容请求
			/// </summary>
			/// <param name="content">请求内容</param>
			/// <returns></returns>
			public String CheckContent(string content)
			{
				string sUrl = null; //请求url 地址
				string sMsg = null; //请求返回参数

				try
				{

					string encode = "GB2312"; //// 支持GB2312, UTF-8
											  //内容和用户名需要进行gb2312或者utf-8编码
					content = System.Web.HttpUtility.UrlEncode(content, Encoding.GetEncoding(encode));
					comName = System.Web.HttpUtility.UrlEncode(comName, System.Text.Encoding.GetEncoding(encode));
					//gb2312调用地址
					sUrl = "http://180.76.112.107/send/checkcontentv2.asp?name=" + comName + "&pwd=" + comPwd + "&content=" + content;
					//utf8调用地址
					//sUrl = "http://180.76.112.107/send/checkcontentv2_utf8.asp?name=" + comName + "&pwd=" + comPwd + "&content=" + content;
					sMsg = GetUrl(sUrl, encode);

					return sMsg;

				}
				catch (Exception e)
				{
					return sMsg;
				}

			}

			/// <summary>
			/// 获取上行短信
			/// </summary>
			/// <returns></returns>
			public String GetUpSms()
			{
				string sUrl = null; //请求url 地址
				string sMsg = null; //请求返回参数

				try
				{
					string encode = "GB2312"; //// 支持GB2312, UTF-8
											  //用户名需要进行gb2312或者utf-8编码
					comName = System.Web.HttpUtility.UrlEncode(comName, System.Text.Encoding.GetEncoding(encode));
					//gb2312调用地址
					sUrl = "http://180.76.112.107/send/readsmsv2.asp?name=" + comName + "&pwd=" + comPwd;
					//utf8调用地址
					//sUrl = "http://180.76.112.107/send/readsmsv2_utf8.asp?name=" + comName + "&pwd=" + comPwd;
					sMsg = GetUrl(sUrl, encode);
					return sMsg;

				}
				catch (Exception e)
				{
					return sMsg;
				}

			}

			/// <summary>
			/// 获取状态报告
			/// </summary>
			/// <returns></returns>
			public String GetStatusReport()
			{

				string sUrl = null; //请求url 地址
				string sMsg = null; //请求返回参数

				try
				{
					string encode = "GB2312"; //// 支持GB2312, UTF-8
											  //用户名需要进行gb2312或者utf-8编码
					comName = System.Web.HttpUtility.UrlEncode(comName, System.Text.Encoding.GetEncoding(encode));
					//gb2312调用地址
					sUrl = "http://180.76.112.107/send/readreportv2.asp?name=" + comName + "&pwd=" + comPwd;
					//utf8调用地址
					//sUrl = "http://180.76.112.107/send/readreportv2_utf8.asp?name=" + comName + "&pwd=" + comPwd;
					sMsg = GetUrl(sUrl, encode);
					return sMsg;

				}
				catch (Exception e)
				{
					return sMsg;
				}

			}

		}
	}
}
