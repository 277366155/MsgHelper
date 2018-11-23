using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MH.Common
{
    public   class ValidateCodeImgHelper
    {
        private const string ValCodeImgDirPath = "/ValidateImages";
        /// <summary>
        ///     创建验证码的图片
        /// </summary>
        /// <param name="validateCode">验证码</param>
        public static byte[] CreateValidateGraphic(string validateCode)
        {
            var image = new Bitmap(120, 50);
            //Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            var g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                var random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                Color[] colors ={
                    Color.DarkSalmon ,
                    Color.CornflowerBlue,
                    Color.LightSeaGreen,
                    Color.Black,
                    Color.Crimson,
                    Color.BlueViolet,
                    Color.Brown,
                    Color.YellowGreen};
                //画图片的干扰线
                for (var i = 0; i < 30; i++)
                {
                    var x1 = random.Next(image.Width);
                    var x2 = random.Next(image.Width);
                    var y1 = random.Next(image.Height);
                    var y2 = random.Next(image.Height);
                    var color = colors[random.Next(colors.Length)];
                    g.DrawLine(new Pen(color,random.Next(3)), x1, y1, x2, y2);
                }
                
                var font = new Font("微软雅黑", 24, FontStyle.Bold | FontStyle.Italic);
               
                //渐变色笔刷
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                    colors[random.Next(colors.Length)], colors[random.Next(colors.Length)], 32.6f, true);
                g.DrawString(validateCode, font, brush, 5, 5);
                //画图片的前景干扰点
                for (var i = 0; i < 100; i++)
                {
                    var x = random.Next(image.Width);
                    var y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(colors[random.Next(colors.Length)]), 0, 0, image.Width - 1, image.Height - 1);

                //保存图片数据
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }


        public static string GetValCodeImg(string valCode, string dirPath = ValCodeImgDirPath)
        {
            
            var basePath = Directory.GetCurrentDirectory();
            dirPath = basePath + dirPath;
            var todayChildDir = dirPath+"/"+ DateTime.Now.ToString("yyyyMMdd");
            Console.WriteLine(todayChildDir);
            if (Directory.Exists(dirPath))
            {
                //获取子文件夹
                var childDirs = Directory.GetDirectories(dirPath);
                foreach (var dir in childDirs)
                {
                    if (!dir.Equals(todayChildDir))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(dir);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(dir);
                    }
                   
                }
            }
            else
            {
                Directory.CreateDirectory(todayChildDir);
            }

            return "";
        }
    }
}
