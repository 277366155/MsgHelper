using MH.Common;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MH.ConsoleTest
{
    class Program
    {
       
        static void Main(string[] args)
        {
			UTF8Base64EncodeAndDecode();

			Console.Read();
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
				var encodStr = Common.DEncrypt.Encrypt(input, Encoding.UTF8, key);
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
           var imgBytes= ValidateCodeImgHelper.CreateValidateGraphic("51s5t");
            FileHelper.CreateFileByBytes(DateTime.Now.ToString("yyyyMMddHHmmss")+".jpeg",imgBytes);
        }

        private static async  Task SendMailTest(Action action)
        {
            action();

            var body = FileHelper.FileReadText("/template.html");
            body= body.Replace("{code}","651368");
            var toMail = "277366155@qq.com";
            var title = "注册验证码";
            EmailHelper.SendMailAsync(toMail, title, body);
        }
    }
}
