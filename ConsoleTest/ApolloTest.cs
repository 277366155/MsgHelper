using Com.Ctrip.Framework.Apollo;
using MH.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace MH.ConsoleTest
{
    public class ApolloTest
    {
        private  IConfigurationBuilder Config;
        public ApolloTest()
        {
            Config = BaseCore.InitConfigurationBuilder(a =>
            a.AddJsonFile("appsettings02.json")
            .AddApollo(BaseCore.Configuration.GetSection("apollo"))
            .AddDefault()
            .AddNamespace("TEST1.booTest"));
        }

        private IConfigurationRoot Root => BaseCore.Configuration;

        public T GetApolloConfig<T>(string key)
        {
            return Root.GetSection(key).Get<T>();
        }

        public void RunTest()
        {
            while (true)
            {
                Console.Write("请输入key:");
                var key = Console.ReadLine();

                Console.WriteLine(GetApolloConfig<string>(key)+"\r\n");                
            }
        }

    }
}
