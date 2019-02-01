using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WxPayAPI;
using Xiaowei.Model;

namespace Xiaowei.API
{
    /// <summary>
    /// 图片上传业务
    /// </summary>
    public class UpFile
    {
        /// <summary>
        /// 上传图片或文件 不能大于2M
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string Upfile(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            byte[] bb = new byte[file.Length];
            file.Read(bb, 0, (int)file.Length);
            file.Close();
            string fileName = Path.GetFileName(filePath);
            MsMultiPartFormData form = new MsMultiPartFormData();
            string decodeName = HttpUtility.UrlDecode(Path.GetFileName(fileName));//最终服务器会按原文件名保存文件，所以需要将文件名编码下，防止中文乱码
            string fileKeyName = "media";
            form.AddStreamFile(fileKeyName, fileName, bb);

            String hashMd5 = Lib.HashHelper.ComputeMD5(filePath);

            WxPayData inputObj = new WxPayData();
            inputObj.SetValue("mch_id", WxPayConfig.GetConfig().GetMchID());
            inputObj.SetValue("media_hash", hashMd5);
            inputObj.SetValue("sign_type", "HMAC-SHA256");
            //inputObj.SetValue("sign", inputObj.MakeSign());//签名

            form.AddFormField("mch_id", WxPayConfig.GetConfig().GetMchID());
            form.AddFormField("media_hash", hashMd5);
            form.AddFormField("sign_type", "HMAC-SHA256");
            form.AddFormField("sign", inputObj.MakeSign());//签名

            string SERVICE_URL = "https://api.mch.weixin.qq.com/secapi/mch/uploadmedia";//最终接收文件上传的服务接口

            string rst = Lib.HttpService.Post(inputObj.ToXml(), SERVICE_URL, form, true, 10);

            inputObj = new WxPayData();
            inputObj.FromXml(rst);

            if (inputObj.GetValue("return_code").ToString() == "SUCCESS")
            {
                return inputObj.GetValue("media_id").ToString();
            }

            return inputObj.GetValue("return_msg").ToString();
        }
    }
}
