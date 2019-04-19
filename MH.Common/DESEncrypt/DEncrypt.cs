using System;
using System.Security.Cryptography;
using System.Text;
namespace MH.Common
{
	/// <summary>
	/// Encrypt ��ժҪ˵����
	/// Copyright (C) Maticsoft
	/// </summary>
	public class DEncrypt
	{
		private const string Key = "MsgHelper20180422";
		/// <summary>
		/// ���췽��
		/// </summary>
		public DEncrypt()
		{
		}


		#region ʹ�� ������Կ�ַ��� ����/����string
		/// <summary>
		/// ʹ�ø�����Կ�ַ�������string
		/// </summary>
		/// <param name="original">ԭʼ����</param>
		/// <param name="key">��Կ</param>
		/// <param name="encoding">�ַ����뷽��</param>
		/// <returns>����</returns>
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
		/// ʹ�ø�����Կ�ַ�������string
		/// </summary>
		/// <param name="original">����</param>
		/// <param name="encoding">���뷽ʽ</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
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
		/// ʹ�ø�����Կ�ַ�������string,����ָ�����뷽ʽ����
		/// </summary>
		/// <param name="encrypted">����</param>
		/// <param name="key">��Կ</param>
		/// <param name="encoding">�ַ����뷽��</param>
		/// <returns>����</returns>
		public static string Decrypt(string encrypted, string key, Encoding encoding)
		{
			byte[] buff = Convert.FromBase64String(encrypted);
			if (string.IsNullOrWhiteSpace(key))
				return encoding.GetString(buff);
			byte[] kb = encoding.GetBytes(key);
			return encoding.GetString(Decrypt(buff, kb));
		}
		#endregion

		#region ʹ�� ȱʡ��Կ�ַ��� ����/����/byte[]
		/// <summary>
		/// ʹ��ȱʡ��Կ�ַ�������byte[]
		/// </summary>
		/// <param name="encrypted">����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
		public static byte[] Decrypt(byte[] encrypted)
		{
			byte[] key = System.Text.Encoding.Default.GetBytes(Key);
			return Decrypt(encrypted, key);
		}
		/// <summary>
		/// ʹ��ȱʡ��Կ�ַ�������
		/// </summary>
		/// <param name="original">ԭʼ����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
		public static byte[] Encrypt(byte[] original)
		{
			byte[] key = System.Text.Encoding.Default.GetBytes(Key);
			return Encrypt(original, key);
		}
		#endregion

		#region  ʹ�� ������Կ ����/����/byte[]

		/// <summary>
		/// ����MD5ժҪ
		/// </summary>
		/// <param name="original">����Դ</param>
		/// <returns>ժҪ</returns>
		public static byte[] MakeMD5(byte[] original)
		{
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
			byte[] keyhash = hashmd5.ComputeHash(original);
			hashmd5 = null;
			return keyhash;
		}


		/// <summary>
		/// ʹ�ø�����Կ����
		/// </summary>
		/// <param name="original">����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
		public static byte[] Encrypt(byte[] original, byte[] key)
		{
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
			des.Key = MakeMD5(key);
			des.Mode = CipherMode.ECB;

			return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
		}

		/// <summary>
		/// ʹ�ø�����Կ��������
		/// </summary>
		/// <param name="encrypted">����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
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
