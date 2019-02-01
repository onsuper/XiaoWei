using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiaowei.Model
{
    public class MsMultiPartFormData
    {
        private List<byte> formData;
        public String Boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
        private String fieldName = "Content-Disposition: form-data; name=\"{0}\"";
        private String fileContentType = "Content-Type: {0}";
        private String fileField = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"";
        private Encoding encode = Encoding.UTF8;
        public MsMultiPartFormData()
        {
            formData = new List<byte>();
        }
        public void AddFormField(String FieldName, String FieldValue)
        {
            String newFieldName = fieldName;
            newFieldName = string.Format(newFieldName, FieldName);
            formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
            formData.AddRange(encode.GetBytes(newFieldName + "\r\n\r\n"));
            formData.AddRange(encode.GetBytes(FieldValue + "\r\n"));
        }
        public void AddFile(String FieldName, String FileName, byte[] FileContent, String ContentType)
        {
            String newFileField = fileField;
            String newFileContentType = fileContentType;
            newFileField = string.Format(newFileField, FieldName, FileName);
            newFileContentType = string.Format(newFileContentType, ContentType);
            formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
            formData.AddRange(encode.GetBytes(newFileField + "\r\n"));
            formData.AddRange(encode.GetBytes(newFileContentType + "\r\n\r\n"));
            formData.AddRange(FileContent);
            formData.AddRange(encode.GetBytes("\r\n"));
        }
        public void AddStreamFile(String FieldName, String FileName, byte[] FileContent)
        {
            AddFile(FieldName, FileName, FileContent, "application/octet-stream");
        }
        public void PrepareFormData()
        {
            formData.AddRange(encode.GetBytes("--" + Boundary + "--"));
        }
        public List<byte> GetFormData()
        {
            return formData;
        }
    }
}
