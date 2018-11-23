using MH.Common;
using System;
using System.Threading;
using System.Xml.Serialization;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine($"生成第{i+1}张验证码");
            //    ImgTest();
            //}
            //Console.WriteLine("生成验证码完成");
            T();
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

        private static void Test()
        {
            var body = FileHelper.FileReadText("/template.html");
            body= body.Replace("{code}","651368");
            var toMail = "277366155@qq.com";
            var title = "注册验证码";
            EmailHelper.SendMailAsync(toMail, title, body);
        }
    }
}
