using System.IO;
using System.Text;

namespace MH.Common
{
    public  class FileHelper
    {
        public static string FileReadText(string path)
        {
            var basePath = Directory.GetCurrentDirectory();
            using (var fileStream = File.OpenRead(basePath + path))
            {
                var length = (int)fileStream.Length;
                byte[] bytes= new byte[length];
                int r = fileStream.Read(bytes, 0, length);
                var str = Encoding.UTF8.GetString(bytes);
                return str;
            }
        }
    }
}
