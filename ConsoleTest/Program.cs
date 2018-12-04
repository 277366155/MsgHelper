using MH.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MH.ConsoleTest
{
    class Program
    {
       
        static void Main(string[] args)
        {
            TaskTest.PrintTaskId("进入Main");
            TaskTest.PrintAsync();
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Main结束，i="+i);
            }
            Console.Read();
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
