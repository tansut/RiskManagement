using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading;

namespace Kalitte.BI.ServerPlatform.Messaging
{
    public class MailManager
    {
        public string Subject;
        public bool UseAutoInfo = true;
        public ArrayList BCCList = new ArrayList();
        public string Body = null;
        public FileTemplate Template = null;
        public string To;
        public string SMTPServer;

        private MailMessage message;

        public MailMessage Message
        {
            get
            {
                return message;
            }
        }

        public MailManager()
        {
            SMTPServer = ConfigurationSettings.AppSettings["SMTPServer"];
            message = new MailMessage();            
        }

        public string GetHTMLFileContents()
        {
            return "";
        }

        public virtual string GetBody()
        {
            if (Body != null)
                return Body;
            else
            {
                    return Template.GetContents();
            }
        }

        public void Send()
        {
            message.IsBodyHtml  = true;
            message.To.Add(this.To);


            message.From = new MailAddress(ConfigurationManager.AppSettings["SMTPFromAddress"], 
                ConfigurationManager.AppSettings["SMTPFromTitle"]);
            message.Subject = this.Subject;
            message.Body = GetBody();

            SmtpClient cl = new SmtpClient(SMTPServer, 587);
            //SmtpClient cl = new SmtpClient(SMTPServer);
            cl.UseDefaultCredentials = false;
            cl.EnableSsl = true;
            cl.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);

            int tryCount = 1;

            while (tryCount++ < 3)
            {
                try
                {
                    cl.Send(message);
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }

        }
    }
}
