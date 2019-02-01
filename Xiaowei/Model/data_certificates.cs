using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaowei.Model
{
    public abstract class result
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class data_certificates//: result
    {
        public List<data_certificate> data { get; set; }
    }

    public class data_certificate
    {
        public string serial_no { get; set; }
        public string effective_time { get; set; }
        public string expire_time { get; set; }
        public encrypt_certificate encrypt_certificate { get; set; }

    }

    public class encrypt_certificate
    {
        public string algorithm { get; set; }
        public string nonce { get; set; }
        public string associated_data { get; set; }
        public string ciphertext { get; set; }
    }
}
