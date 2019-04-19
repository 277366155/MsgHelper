using System;
using System.Security.Cryptography;
using System.Text;
namespace MH.Common
{
	/// <summary>
	/// Encrypt 的摘要说明。
	/// Copyright (C) Maticsoft
	/// </summary>
	public class DEncrypt
	{
		private const string Key = "MsgHelper20180422";
		/// <summary>
		/// 构造方法
		/// </summary>
		public DEncrypt()
		{
		}


		#region 使用 给定密钥字符串 加密/解密string
		/// <summary>
		/// 使用给定密钥字符串加密string
		/// </summary>
		/// <param name="original">原始文字</param>
		/// <param name="key">密钥</param>
		/// <param name="encoding">字符编码方案</param>
		/// <returns>密文</returns>
		public static string Encrypt(string original, Encoding encoding = null, string key = Key)
		{
			try
			{
				if (encoding == null)
					encoding = Encoding.Default;

				byte[] buff = encoding.GetBytes(original);
				if (string.IsNullOrWhiteSpace(key))
					return Convert.ToBase64String(buff);

				byte[] kb = encoding.GetBytes(key);
				return Convert.ToBase64String(Encrypt(buff, kb));
			}
			catch
			{
				return "";
			}
		}


		/// <summary>
		/// 使用给定密钥字符串解密string
		/// </summary>
		/// <param name="original">密文</param>
		/// <param name="encoding">编码方式</param>
		/// <param name="key">密钥</param>
		/// <returns>明文</returns>
		public static string Decrypt(string original, Encoding encoding = null, string key = Key)
		{
			try
			{
				if (encoding == null)
					encoding = Encoding.Default;

				return Decrypt(original, key, encoding);
			}
			catch
			{
				return "";
			}
		}

		/// <summary>
		/// 使用给定密钥字符串解密string,返回指定编码方式明文
		/// </summary>
		/// <param name="encrypted">密文</param>
		/// <param name="key">密钥</param>
		/// <param name="encoding">字符编码方案</param>
		/// <returns>明文</returns>
		public static string Decrypt(string encrypted, string key, Encoding encoding)
		{
			byte[] buff = Convert.FromBase64String(encrypted);
			if (string.IsNullOrWhiteSpace(key))
				return encoding.GetString(buff);
			byte[] kb = encoding.GetBytes(key);
			return encoding.GetString(Decrypt(buff, kb));
		}
		#endregion

		#region 使用 缺省密钥字符串 加密/解密/byte[]
		/// <summary>
		/// 使用缺省密钥字符串解密byte[]
		/// </summary>
		/// <param name="encrypted">密文</param>
		/// <param name="key">密钥</param>
		/// <returns>明文</returns>
		public static byte[] Decrypt(byte[] encrypted)
		{
			byte[] key = System.Text.Encoding.Default.GetBytes(Key);
			return Decrypt(encrypted, key);
		}
		/// <summary>
		/// 使用缺省密钥字符串加密
		/// </summary>
		/// <param name="original">原始数据</param>
		/// <param name="key">密钥</param>
		/// <returns>密文</returns>
		public static byte[] Encrypt(byte[] original)
		{
			byte[] key = System.Text.Encoding.Default.GetBytes(Key);
			return Encrypt(original, key);
		}
		#endregion

		#region  使用 给定密钥 加密/解密/byte[]

		/// <summary>
		/// 生成MD5摘要
		/// </summary>
		/// <param name="original">数据源</param>
		/// <returns>摘要</returns>
		public static byte[] MakeMD5(byte[] original)
		{
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
			byte[] keyhash = hashmd5.ComputeHash(original);
			hashmd5 = null;
			return keyhash;
		}


		/// <summary>
		/// 使用给定密钥加密
		/// </summary>
		/// <param name="original">明文</param>
		/// <param name="key">密钥</param>
		/// <returns>密文</returns>
		public static byte[] Encrypt(byte[] original, byte[] key)
		{
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
			des.Key = MakeMD5(key);
			des.Mode = CipherMode.ECB;

			return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
		}

		/// <summary>
		/// 使用给定密钥解密数据
		/// </summary>
		/// <param name="encrypted">密文</param>
		/// <param name="key">密钥</param>
		/// <returns>明文</returns>
		public static byte[] Decrypt(byte[] encrypted, byte[] key)
		{
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
			des.Key = MakeMD5(key);
			des.Mode = CipherMode.ECB;

			return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
		}

		#endregion




	}
}
