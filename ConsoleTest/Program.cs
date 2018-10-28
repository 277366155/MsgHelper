using MH.Common;
using MH.Context;
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
            Test();
            Console.Read();
        }

        private static void Test()
        {
            new S("我是谁。。。");
        }
    }

    public class P
    {
        protected string str;
        public P()
        {
            Console.WriteLine("父类无参构造函数");
            Con();
        }
        public P(string s)
        {
            Console.WriteLine("父类有参构造函数："+s);
            Con();
        }

        public virtual void Con()
        {
            Console.WriteLine("Con参数：" + str);
        }
    }

    public class S : P
    {
        private string ss;
        public S()
        {
            Console.WriteLine("子类无参构造函数");
        }

        public S(string s)
        {
            ss = s;
            Console.WriteLine("子类有参构造函数："+s);
        }

        public override void Con()
        {
            base.str = ss;
            base.Con();
        }
    }
}
