using MH.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfig();
            var pwd = "zb871225123";
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(pwd);
                pwd = Convert.ToBase64String(md5.ComputeHash(bytes));
            }
            var data = WxApi.AddCustomerService(new CustomServiceParam() { kf_account = "boo@MsgHelper", nickname = "阿花", password =pwd });
            Console.WriteLine(data.ObjToJson());
            Console.Read();
        }

        private static void InitConfig()
        {
            var conf = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true, true)
                  .AddJsonFile("appsettings.Development.json", true, true)
                  .Build();
        }
    }
}
