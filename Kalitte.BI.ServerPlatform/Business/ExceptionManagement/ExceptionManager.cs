using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.Shared.Exceptions;
using System.Web;
using System.Threading;
using Kalitte.BI.ServerPlatform.Messaging;

namespace Kalitte.BI.ServerPlatform.Business.ExceptionManagement
{
    public static class ExceptionManager
    {
        private static ApplicationException Convert(Exception exc)
        {
            if (exc is TechnicalException)
                return (TechnicalException)exc;
            else if (exc is BusinessException)
                return (BusinessException)exc;
            else if (exc is FormatException)
                return new BusinessException("Lütfen girdiğiniz değerleri kontrol ediniz. Bunun" +
                                              "bir hata sonucu oluştuğunu düşünüyorsanız teknik desteğe başvurunuz.");
            return new TechnicalException("Sistemde yakalanamayan bir hata oluştu. Lütfen görevlilerle irtibata geçiniz", exc);
        }

        private static void Publish(Exception exc)
        {
            Exception temp = exc;
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(string.Format("İstisna Tipi: {0}, Kullanıcı: {1}\n",
                    temp.GetType().Name, Thread.CurrentPrincipal.Identity.Name));
                sb.Append(string.Format("Mesaj: {0}\n", temp.Message));
                sb.Append(string.Format("StackTrace: {0}", temp.StackTrace));
                sb.Append("------------------------------------------------\n\n");

                temp = temp.InnerException;

            } while (temp != null);

            MailManager m = new MailManager();
            m.To = "bilgi@kalitte.com.tr";
            m.Body = sb.ToString();
            m.Subject = "AnaliticsPortal.com";
            try
            {
                m.Send();
            }
            catch
            {
            }
        }

        public static ApplicationException Manage(Exception exc)
        {
            if (exc is HttpUnhandledException && exc.InnerException != null)
                exc = exc.InnerException;


            ApplicationException cExc = Convert(exc);


            if (cExc is TechnicalException)
                Publish(cExc);

            return cExc;
        }

    }
}
