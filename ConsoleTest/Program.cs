using MH.Common;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.Read();
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
