using Com.Ctrip.Framework.Apollo;
using MH.Common;
using MH.ConsoleTest.Common;
using MH.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MH.ConsoleTest
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine("启动成功。。。。");
			//TraceListener();
			//new ApolloTest().RunTest();
			//Sms.SendMsg();
			//UTF8Base64EncodeAndDecode();
			//ConfigBuilderTest();
			RabbitMqTest();

			//SigneCheck();
			//SendMailTest(null);
			Console.Read();
		}

		public static void SigneCheck()
		{
			var data = "appId=Boo_Test&businessParam={'id':3244002}&requestId=123df&timestamp=1561790792662&vendorId=Boo";
			var signResult = SSLHelper.GetSignResult(data);
			Console.WriteLine($"签名结果：[{signResult}]");

			var check = SSLHelper.CheckSign(signResult, data);
			Console.WriteLine($"公钥验证签名：[{check}]");
		}
		private static void RabbitMqTest()
		{
			var rabb = new RabbitMqTest();
			//rabb.Consumer();
			rabb.Consumer2();
			while (true)
			{
				Console.WriteLine("输入年龄和姓名。。");
				rabb.Publisher();
			}
		}

		private static void TraceListener()
		{
			Trace.Listeners.Clear();
			Trace.Listeners.Add(new CustomTraceListener());
			while (true)
			{
				Console.WriteLine("输入记录：");
				var txt = Console.ReadLine();
				Trace.TraceInformation("TraceInformation：" + txt);
				Trace.TraceWarning("TraceWarning：" + txt);
				Trace.TraceError("TraceError：" + txt);
				Console.WriteLine("\r——————————————————————");
			}
		}
		private static void ConfigBuilderTest()
		{
			var config1 = BaseCore.Configuration;
			//Console.WriteLine("1 :"+config1.GetValue<dynamic>("apollo").ObjToJson());
			Console.WriteLine("1 :" + config1.GetSection("Logging:IncludeScopes").ToJson());
			BaseCore.InitConfigurationBuilder((a) =>
			{
				a.AddJsonFile("appsettings02.json", optional: true, reloadOnChange: true);
			});
			var config2 = BaseCore.Configuration;
			Console.WriteLine("2 :" + config1.GetSection("Logging:IncludeScopes").ToJson());
			Console.WriteLine("2 :" + config2.GetSection("apollo:appid").Value);
		}

		private static void UTF8Base64EncodeAndDecode()
		{
			while (true)
			{
				Console.WriteLine("--------------------\r\n 退出输入Q：");
				if (Console.ReadLine().ToLower() == "q")
				{
					return;
				}
				Console.WriteLine("请输入要编码的字符串：");
				var input = Console.ReadLine();
				Console.WriteLine("请输入编码的key：");
				var key = Console.ReadLine();
				if (key.IsNullOrWhiteSpace())
				{
					key = null;
				}
				var encodStr = DEncrypt.Encrypt(input, Encoding.UTF8, key);
				Console.WriteLine($"编码结果：{encodStr}");

			}
		}

		private static void TaskTestMethod()
		{
			TaskTest.PrintTaskId("进入Main");
			TaskTest.PrintAsync();
			for (var i = 0; i < 5; i++)
			{
				Thread.Sleep(500);
				Console.WriteLine("Main结束，i=" + i);
			}
		}


		public static void T()
		{
			ValidateCodeImgHelper.GetValCodeImg("13265");
		}

		public static void ImgTest()
		{
			var imgBytes = ValidateCodeImgHelper.CreateValidateGraphic("51s5t");
			FileHelper.CreateFileByBytes(DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg", imgBytes);
		}

		private static void SendMailTest(Action action)
		{
			action?.Invoke();

			var body = FileHelper.FileReadText("/template.html");
			body = body.Replace("{code}", "651368");
			var toMail = "277366155@qq.com";
			var title = "注册验证码";
			EmailHelper.SendMailAsync(toMail, title, body);
		}
	}
}
