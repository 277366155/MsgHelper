using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/*
 * 2018.12.04：异步方法的使用示例
 */
namespace MH.ConsoleTest
{
    public class TaskTest
    { 
        //计时器
        private static readonly Stopwatch sw = new Stopwatch();
        public static async Task PrintAsync()
        {
            PrintTaskId("进入PrintAsync");
            sw.Start();
            await Task.Run(() =>
            {
                PrintTaskId("Task.Run 1 内部");
                Console.WriteLine("Task.Run 1 sleep 1 second.");
                Thread.Sleep(1000);
            });
            PrintTaskId("Task.Run 1 结束");
            await Task.Run(() =>
            {
                PrintTaskId("Task.Run 2 内部");
                Console.WriteLine("Task.Run 2 sleep 2 second.");
                Thread.Sleep(2000);
            });
            PrintTaskId("Task.Run 2 结束，PrintAsync结束");
            Console.WriteLine("执行毫秒数：" + sw.ElapsedMilliseconds);
        }
        public static void PrintTaskId(string msg)
        {
            Console.WriteLine(msg + "，当前线程id：" + Thread.GetCurrentProcessorId());
        }
    }
}
