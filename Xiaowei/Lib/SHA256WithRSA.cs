using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Xiaowei.Lib
{
    public class SHA256WithRSA
    {
        //public string pubKeyFile;

        //public static string Sign(string contentForSign, string priKeyFile, string keyPwd)
        //{
        //    var rsa = GetPrivateKey(priKeyFile, keyPwd);
        //    // Create a new RSACryptoServiceProvider
        //    var rsaClear = new RSACryptoServiceProvider();
        //    // Export RSA parameters from 'rsa' and import them into 'rsaClear'
        //    var paras = rsa.ExportParameters(true);
        //    rsaClear.ImportParameters(paras);
        //    using (var sha256 = new SHA256CryptoServiceProvider())
        //    {
        //        var signData = rsa.SignData(Encoding.UTF8.GetBytes(contentForSign), sha256);
        //        return BytesToHex(signData);
        //    }
        //}

        //public bool VerifySign(string contentForSign, string signedData, string pubKeyFile)
        //{
        //    var rsa = GetPublicKey(pubKeyFile);

        //    using (var sha256 = new SHA256CryptoServiceProvider())
        //    {
        //        return rsa.VerifyData(Encoding.UTF8.GetBytes(contentForSign), sha256, HexToBytes(signedData));
        //    }
        //}

        ///// <summary>
        ///// 获取签名证书私钥
        ///// </summary>
        ///// <param name="priKeyFile"></param>
        ///// <param name="keyPwd"></param>
        ///// <returns></returns>
        //private static RSACryptoServiceProvider GetPrivateKey(string priKeyFile, string keyPwd)
        //{
        //    var pc = new X509Certificate2(priKeyFile, keyPwd, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
        //    return (RSACryptoServiceProvider)pc.PrivateKey;
        //}

        ///// <summary>
        ///// 获取验签证书
        ///// </summary>
        ///// <param name="pubKeyFile"></param>
        ///// <returns></returns>
        //private static RSACryptoServiceProvider GetPublicKey(string pubKeyFile)
        //{
        //    var pc = new X509Certificate2(pubKeyFile);
        //    return (RSACryptoServiceProvider)pc.PublicKey.Key;
        //}

        //public static byte[] HexToBytes(string text)
        //{
        //    if (text.Length % 2 != 0)
        //        throw new ArgumentException("text 长度为奇数。");

        //    List<byte> lstRet = new List<byte>();
        //    for (int i = 0; i < text.Length; i = i + 2)
        //    {
        //        lstRet.Add(Convert.ToByte(text.Substring(i, 2), 16));
        //    }
        //    return lstRet.ToArray();
        //}

        ///// <summary>
        ///// bytes转换hex
        ///// </summary>
        ///// <param name="data">bytes</param>
        ///// <returns>转换后的hex字符串</returns>
        //public static string BytesToHex(byte[] data)
        //{
        //    StringBuilder sbRet = new StringBuilder(data.Length * 2);
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sbRet.Append(Convert.ToString(data[i], 16).PadLeft(2, '0'));
        //    }
        //    return sbRet.ToString();
        //}

        public static string RSA(string str, string pfxFilePath, string pfxPassword)
        {
            //SHA256WithRSA  
            X509Certificate2 privateCert = new X509Certificate2(pfxFilePath, pfxPassword, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserKeySet);
            RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;
            // This one can:  
            RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
            privateKey1.ImportParameters(privateKey.ExportParameters(true));
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] signature = privateKey1.SignData(data, "SHA256");
            //密文  
            string sign = Convert.ToBase64String(signature);

            return sign;

        }
    }
}
