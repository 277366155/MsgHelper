using System;
using System.Collections.Generic;
using System.Text;

namespace MH.ConsoleTest.Common
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	public static class SSLHelper
	{
		private static string PrivateKey = @"MIICXAIBAAKBgQDiso4bz4VJWn+M1aBQkprovgd3qgCckKv6OITB1aKyxZ/Xp+wi
8VXReEQxNgF2EGObs/Ltu9kEYBnfxrs0QPiNEd5X4hYkeRxQAuNmFLV2dcAXkR4M
dy4Dwcsiw6vArmOv0THDntWz9PMJrxpowMoem9fd7cOEeHVSK4zfafjDewIDAQAB
AoGBAMy3DZmTpvt8294kM+dO3ND8eeXYAUFha8xEKa6Y65mg2R14KMfNRAArKPl/
mYYyeqDquZ9xmSJYXkU0Q22Glmuq4fjTdQgQDDhyNSxn3qBDaKY7YMF5/tvfrkJw
u1o5BUmDpm058PgNlBDy2bwjcQH3rwkE8bjfwfsOqexrqk9BAkEA9WmrblmyWRjq
nl1EnmTmbo7WHZN+tyN6O4FzrLRoRHo5ERhED+bMmmI7QLar6AuLL7wIHPbemkc3
H1cYwM5sKwJBAOx6McVRe8Q9QPFnLHzv5rsnAWWrQHlDewFgrbODEWtyA2qHsck7
vFrDnPEiKB4P6vG9nqe4TQe4R8WjKHlUTfECQHGAC58fsNJwKaJQdHnlJIWhXfmT
y5kbuV5oAn2vekGhXV9An8nS7nHAWLMXSO4q2JadGgt7SXyEz0OZXoNddL0CQAev
iQKSvWUJYJz373g4C9W1VNRLFpNaYBsRW1PkRKKrV/UUZ/DUYjDI/sbPh2JCvi4R
LaDh2o8PrDV+MgiPduECQHaWRcy6zjKbIVAPKo82iR5qUu5GP1grmvMCqJ8WtH+i
6zMi48XUrJRSWnFzWOFqvy4sgAwFUNCMuTReRkCW3EY=".Replace("\n", "").Replace("\r","");

		private static string PublicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDiso4bz4VJWn+M1aBQkprovgd3
qgCckKv6OITB1aKyxZ/Xp+wi8VXReEQxNgF2EGObs/Ltu9kEYBnfxrs0QPiNEd5X
4hYkeRxQAuNmFLV2dcAXkR4Mdy4Dwcsiw6vArmOv0THDntWz9PMJrxpowMoem9fd
7cOEeHVSK4zfafjDewIDAQAB".Replace("\n", "").Replace("\r", "");

		static SSLHelper()
		{
			PrintKeys();
		}

		private static void PrintKeys()
		{
			Console.WriteLine("PrivateKey：" + PrivateKey);
			Console.WriteLine("PublicKey：" + PublicKey);
		}

		#region 公钥加密，私钥解密
		/// <summary>
		/// 使用公钥对数据加密
		/// </summary>
		/// <param name="jsonStr">要加密的数据</param>
		/// <returns></returns>
		public static string GetEcrypedResultByPublicKey(string jsonStr)
		{
			var result = "";
			var value = Encoding.UTF8.GetBytes(jsonStr);
			SHA1Managed sha1 = new SHA1Managed();
			#region 公钥加密
			var rsa = CreateRsaFromPublicKey(PublicKey);
			RSAPKCS1KeyExchangeFormatter ef = new RSAPKCS1KeyExchangeFormatter(rsa);
			var signature = ef.CreateKeyExchange(value);
			#endregion
			result = Convert.ToBase64String(signature).Replace("+", "%2B");
			return result;
		}

		/// <summary>
		/// 通过私钥对公钥加密数据解密
		/// </summary>
		/// <param name="paramStr"></param>
		/// <returns></returns>
		public static string GetDecrypedResultByPrivateKey(string paramStr)
		{
			//base64转bytes
			var byteArrary = Convert.FromBase64String(paramStr);
			#region 私钥解密
			var rsa = CreateRsaFromPrivateKey(PrivateKey);
			RSAPKCS1KeyExchangeDeformatter ed = new RSAPKCS1KeyExchangeDeformatter(rsa);
			var value = ed.DecryptKeyExchange(byteArrary);
			#endregion
			return Encoding.UTF8.GetString(value);
		}
		#endregion 公钥加密，私钥解密


		#region 私钥签名，公钥验证
		/// <summary>
		/// 使用私钥进行加密签名
		/// </summary>
		/// <param name="paramStr">原签名字符串</param>
		/// <returns></returns>
		public static string GetSignResult(string paramStr)
		{
			var bytes = Encoding.UTF8.GetBytes(paramStr);
			SHA1Managed sha1 = new SHA1Managed();
			var sha1Bytes = sha1.ComputeHash(bytes);
			var rsa = CreateRsaFromPrivateKey(PrivateKey);
			RSAPKCS1SignatureFormatter sf = new RSAPKCS1SignatureFormatter(rsa);
			sf.SetHashAlgorithm("SHA1");
			var value = sf.CreateSignature(sha1Bytes);
			return Convert.ToBase64String(value).Replace("+", "%2B");
		}

		/// <summary>
		/// 使用公钥对加密签名进行验证
		/// </summary>
		/// <param name="signParam">签名凭证</param>
		/// <param name="dataParam">原签名字符串</param>
		/// <returns></returns>
		public static bool CheckSign(string signParam, string dataParam)
		{
			signParam = signParam.Replace("%2B", "+");
			var signBytes = Convert.FromBase64String(signParam);
			var dataBytes = Encoding.UTF8.GetBytes(dataParam);
			SHA1Managed sha1 = new SHA1Managed();
			var sha1Bytes = sha1.ComputeHash(dataBytes);

			var rsa = CreateRsaFromPublicKey(PublicKey);
			RSAPKCS1SignatureDeformatter sd = new RSAPKCS1SignatureDeformatter(rsa);
			sd.SetHashAlgorithm("SHA1");
			return sd.VerifySignature(sha1Bytes, signBytes);
		}
		#endregion 私钥签名，公钥验证

		#region 获取RSA
		private static RSA CreateRsaFromPrivateKey(string privateKey)
		{
			var privateKeyBits = System.Convert.FromBase64String(privateKey);
			var rsa = RSA.Create();
			var RSAparams = new RSAParameters();

			using (var binr = new BinaryReader(new MemoryStream(privateKeyBits)))
			{
				byte bt = 0;
				ushort twobytes = 0;
				twobytes = binr.ReadUInt16();
				if (twobytes == 0x8130)
					binr.ReadByte();
				else if (twobytes == 0x8230)
					binr.ReadInt16();
				else
					throw new Exception("Unexpected value read binr.ReadUInt16()");

				twobytes = binr.ReadUInt16();
				if (twobytes != 0x0102)
					throw new Exception("Unexpected version");

				bt = binr.ReadByte();
				if (bt != 0x00)
					throw new Exception("Unexpected value read binr.ReadByte()");

				RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
				RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
			}

			rsa.ImportParameters(RSAparams);
			return rsa;
		}

		private static RSA CreateRsaFromPublicKey(string publicKey)
		{
			byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
			byte[] x509key;
			byte[] seq = new byte[15];
			int x509size;

			x509key = Convert.FromBase64String(publicKey);
			x509size = x509key.Length;

			using (var mem = new MemoryStream(x509key))
			{
				using (var binr = new BinaryReader(mem))
				{
					byte bt = 0;
					ushort twobytes = 0;

					twobytes = binr.ReadUInt16();
					if (twobytes == 0x8130)
						binr.ReadByte();
					else if (twobytes == 0x8230)
						binr.ReadInt16();
					else
						return null;

					seq = binr.ReadBytes(15);
					if (!CompareBytearrays(seq, SeqOID))
						return null;

					twobytes = binr.ReadUInt16();
					if (twobytes == 0x8103)
						binr.ReadByte();
					else if (twobytes == 0x8203)
						binr.ReadInt16();
					else
						return null;

					bt = binr.ReadByte();
					if (bt != 0x00)
						return null;

					twobytes = binr.ReadUInt16();
					if (twobytes == 0x8130)
						binr.ReadByte();
					else if (twobytes == 0x8230)
						binr.ReadInt16();
					else
						return null;

					twobytes = binr.ReadUInt16();
					byte lowbyte = 0x00;
					byte highbyte = 0x00;

					if (twobytes == 0x8102)
						lowbyte = binr.ReadByte();
					else if (twobytes == 0x8202)
					{
						highbyte = binr.ReadByte();
						lowbyte = binr.ReadByte();
					}
					else
						return null;
					byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
					int modsize = BitConverter.ToInt32(modint, 0);

					int firstbyte = binr.PeekChar();
					if (firstbyte == 0x00)
					{
						binr.ReadByte();
						modsize -= 1;
					}

					byte[] modulus = binr.ReadBytes(modsize);

					if (binr.ReadByte() != 0x02)
						return null;
					int expbytes = (int)binr.ReadByte();
					byte[] exponent = binr.ReadBytes(expbytes);

					var rsa = RSA.Create();
					var rsaKeyInfo = new RSAParameters
					{
						Modulus = modulus,
						Exponent = exponent
					};
					rsa.ImportParameters(rsaKeyInfo);
					return rsa;
				}
			}
		}

		private static bool CompareBytearrays(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
				return false;
			int i = 0;
			foreach (byte c in a)
			{
				if (c != b[i])
					return false;
				i++;
			}
			return true;
		}

		private static int GetIntegerSize(BinaryReader binr)
		{
			byte bt = 0;
			byte lowbyte = 0x00;
			byte highbyte = 0x00;
			int count = 0;
			bt = binr.ReadByte();
			if (bt != 0x02)
				return 0;
			bt = binr.ReadByte();

			if (bt == 0x81)
				count = binr.ReadByte();
			else if (bt == 0x82)
			{
				highbyte = binr.ReadByte();
				lowbyte = binr.ReadByte();
				byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
				count = BitConverter.ToInt32(modint, 0);
			}
			else
			{
				count = bt;
			}

			while (binr.ReadByte() == 0x00)
			{
				count -= 1;
			}
			binr.BaseStream.Seek(-1, SeekOrigin.Current);
			return count;
		}
		#endregion 获取RSA
	}
}

