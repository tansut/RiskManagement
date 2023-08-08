using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public enum DosyaKayitTip
    {
        Surec,
        Risk,
        Kontrol
    }

    public enum GecerliUzanti
    {
        doc,
        docx,
        pdf,
        zip,
        rar
    }

    public class DosyaEkBusiness : EntityBusiness<DosyaEk>
    {
        public List<DosyaEk> GetDosyaEk(int kayitid, string kayittip)
        {
            return GetQueryable().Where(p => p.KayitID == kayitid && p.KayitTip == kayittip).ToList();
        }

        protected override void InsertingEntity(DosyaEk entity)
        {
            base.InsertingEntity(entity);
            entity.UniqueKey = Guid.NewGuid();
        }

        public DosyaEk GetDosyaEkbyUniqueKey(Guid uniquekey)
        {
            return GetQueryable().Where(p => p.UniqueKey == uniquekey).SingleOrDefault();
        }

        public bool ValidateExtension(string FileExtension)
        {
            return Enum.GetNames(typeof(GecerliUzanti)).Contains(FileExtension.Substring(1));
        }

        public int getPuan(string values)
        {
            Dictionary<string, object>[] rows = JSON.Deserialize<Dictionary<string, object>[]>(values);
            int puan =  Convert.ToInt32(rows.First()["Puan"]);
            return puan;
        }
    }
}
