using System;
using System.Diagnostics;
using System.IO;

namespace MH.Core
{
	public class CustomTraceListener : TraceListener
	{
		private static string LogFileName = $"{DateTime.Now.ToString("yyyy-MM-dd")}.log";
		private static string _dirPath = "";
		private string LogFilePath = $"{DirPath}\\Log\\";

		public static string DirPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_dirPath))
					_dirPath = Directory.GetCurrentDirectory();

				return _dirPath;
			}
		}
		
		public override void Write(string message)
		{
			CreateFile();
			File.AppendAllText(LogFilePath+LogFileName, message);
		}

		public override void WriteLine(string message)
		{
			CreateFile();
			File.AppendAllText(LogFilePath+LogFileName, $"[{DateTime.Now}]\t{message}\r\n");
		}

		private void CreateFile()
		{
			if (!Directory.Exists(LogFilePath))
			{
				Directory.CreateDirectory(LogFilePath);
			}
			if (!File.Exists(LogFilePath + LogFileName))
			{
				File.Create(LogFilePath + LogFileName).Close();				
			}
		}
	}
}
