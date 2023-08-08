using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Business.Surec;
using System.IO;

namespace Kalitte.RiskManagement.Web.Handlers
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    public class DownloadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            Guid id = new Guid(context.Request["Id"]);
            var dosyaek = new DosyaEkBusiness().GetDosyaEkbyUniqueKey(id);
            var File = dosyaek.Icerik;
            if (File == null || File.Length == 0) return;

            string FileExtension = Path.GetExtension(dosyaek.DosyaAd).ToLowerInvariant();

            context.Response.ContentType = WebHelper.GetContentType(FileExtension);
            context.Response.AppendHeader("Content-Disposition",  string.Format("attachment; filename={0}", dosyaek.DosyaAd));

            MemoryStream ms = new MemoryStream(File);
            ms.WriteTo(context.Response.OutputStream);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}