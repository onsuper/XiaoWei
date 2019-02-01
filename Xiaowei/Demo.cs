using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaowei
{
    public class Demo
    {
        /// <summary>
        /// 控制台测试入口
        /// </summary>
        static void Main()
        {
            Console.WriteLine(API.XiaoWei.InfoEncryption("123"));
            Console.ReadLine();

        }

        /// <summary>
        /// DEMO
        /// </summary>
        public string DemoSubmit()
        {

            //WxPayAPI.dll是用提官方dll 可自行下载c#编译源码
            //如果遇到技术问题进群沟通 
            //QQ群:771207642
            //https://pay.weixin.qq.com/wiki/doc/api/micropay_sl.php?chapter=11_1

            Lib.Config.MchId = "商户号";
            Lib.Config.CertPath = "";
            Lib.Config.CertPwd = "";
            Lib.Config.Serial_no = "";



            //使用前先配置上面的参数
            WxPayAPI.WxPayData inputObj = new WxPayAPI.WxPayData();

            inputObj.SetValue("business_code", ""); // //业务申请编号
            inputObj.SetValue("bank_name", ""); // //开户银行全称（含支行）
            inputObj.SetValue("account_bank", ""); // //开户银行

            //https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=19_5
            inputObj.SetValue("bank_address_code", ""); // //开户银行省市编码,
            inputObj.SetValue("id_card_valid_time", ""); // //身份证有效期限
            inputObj.SetValue("merchant_shortname", ""); // //商户简称
            inputObj.SetValue("service_phone", ""); // //客服电话
            inputObj.SetValue("store_name", ""); // //门店名称

            //https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=19_5
            inputObj.SetValue("store_address_code", ""); // //门店省市编码,
            inputObj.SetValue("store_street", ""); // //门店街道名称

            //https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=19_5
            inputObj.SetValue("business", ""); // //经营类目ID,
            inputObj.SetValue("rate", ""); // //商户费率

            // MediaID图片字符串
            //先使用上传图片函数获取结果
            inputObj.SetValue("id_card_copy", ""); // //身份证人像面照片
            inputObj.SetValue("id_card_national", ""); // //身份证国徽面照片
            inputObj.SetValue("store_entrance_pic", ""); // //门店门口照片
            inputObj.SetValue("indoor_pic", ""); // //店内环境照片

            // 敏感信息加密
            inputObj.SetValue("id_card_name", ("")); // //身份证姓名
            inputObj.SetValue("id_card_number", ("")); // //身份证
            inputObj.SetValue("account_name", ("")); // //开户名称
            inputObj.SetValue("account_number", ("")); // //银行账号
            inputObj.SetValue("contact", ("")); // //联系人姓名
            inputObj.SetValue("contact_phone", ("")); // //手机号码

            string result = API.XiaoWei.Submit(inputObj);
            return result;
        }

    }
}
