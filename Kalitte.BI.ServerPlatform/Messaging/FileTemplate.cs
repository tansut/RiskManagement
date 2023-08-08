using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace Kalitte.BI.ServerPlatform.Messaging
{
    public class FileTemplate
    {
        private string TemplateFile;
        private NameValueCollection htmltags = null;

        public NameValueCollection HtmlTags
        {
            get
            {
                if (htmltags == null)
                    htmltags = new NameValueCollection();
                return htmltags;
            }
        }

        public FileTemplate(string FileName)
        {
            this.TemplateFile = FileName;
        }

        public string GetContents()
        {
            using (StreamReader s = new StreamReader(TemplateFile, Encoding.GetEncoding("iso-8859-9")))
            {
                StringBuilder sb = new StringBuilder(s.ReadToEnd());
                for (int i = 0; i < HtmlTags.Count; i++)
                {
                    sb.Replace(HtmlTags.GetKey(i), HtmlTags.Get(i));
                }
                s.Close();
                return sb.ToString();
            }
        }
    }
}
