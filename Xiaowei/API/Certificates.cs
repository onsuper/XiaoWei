using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xiaowei.Lib;
using Xiaowei.Model;

namespace Xiaowei.API
{
    /// <summary>
    /// 下载敏感信息加密证书
    /// </summary>
    public class Certificates
    {
        /// <summary>
        /// 获取证书函数
        /// </summary>
        /// <param name="serial_no"></param>
        /// <returns></returns>
        public static string Get(string serial_no)
        {
            string server = "https://api.mch.weixin.qq.com";
            string path = "/v3/certificates";
            string url = server + path;

            string NonceStr = Guid.NewGuid().ToString().Replace("-", "");
            string TimeStamp = Helper.GenerateTimeStamp();

            string signContent = "GET\n"
                                + path + "\n"
                                + TimeStamp + "\n"
                                + NonceStr + "\n\n";


            string signature = SHA256WithRSA.RSA(signContent,
                                "证书文件.p12", Config.MchId);

            string Authorization = "WECHATPAY2-SHA256-RSA2048 " +
                                "mchid=\"" + Config.MchId + "\"," +
                                "nonce_str=\"" + NonceStr + "\"," +
                                "signature=\"" + signature + "\"," +
                                "timestamp=\"" + TimeStamp + "\"," +
                                "serial_no=\"" + serial_no + "\"";


            System.Net.WebHeaderCollection Headers = new System.Net.WebHeaderCollection
            {
                { "Authorization", Authorization }
            };
            Authorization = HttpService.Get(url, true, 10, "application/json", Headers);

            return Authorization;

            //下载成功的证书
            //string file = "certificates.json";
            //System.IO.File.WriteAllText(file, Authorization);

        }

        /// <summary>
        /// 获取证书结构
        /// </summary>
        /// <returns></returns>
        public static data_certificates Get()
        {
            string Authorization = Get(Lib.Config.Serial_no);
            data_certificates dc = JsonMapper.ToObject<data_certificates>(Authorization);
            return dc;

        }
    }
}
