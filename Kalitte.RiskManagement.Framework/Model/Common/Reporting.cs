using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Collections;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Kalitte.RiskManagement.Framework.Model.Common
{
    public class RiskHistoryReportData
    {
        public int RiskID { get; set; }
        public string RiskAd { get; set; }
        public string SurecAd { get; set; }
        public DateTime OnayTarihi { get; set; }
        public string Etki { get; set; }
        public string Olasilik { get; set; }
        public string ArtikEtki { get; set; }
        public string ArtikOlasilik { get; set; }
        public string Skor { get; set; }
        public string ArtikSkor { get; set; }
        public string SkorRenk { get; set; }
        public string ArtikSkorRenk { get; set; }
    }

    public class RiskReportData
    {
        public int RiskID { get; set; }
        public string RiskAd { get; set; }
        public string RiskDurum { get; set; }
        public string SurecAd { get; set; }
        public string BirimAd { get; set; }
        public string KontrolAd { get; set; }
        public string StratejikHedef { get; set; }
        public string EtkiSkorTanim { get; set; }
        public string OlasilikSkorTanim { get; set; }
        public string SkorTanim { get; set; }
        public double? EtkiPuan { get; set; }
        public double? OlasilikPuan { get; set; }
        public double? SkorPuan { get; set; }
        //public string sEtkiSkorTanim { get; set; }
        //public string sOlasilikSkorTanim { get; set; }
        //public string sSkorTanim { get; set; }
        //public double? sEtkiPuan { get; set; }
        //public double? sOlasilikPuan { get; set; }
        //public double? sSkorPuan { get; set; }
        public DateTime Tarih { get; set; }
        public string KullaniciAd { get; set; }
        public string OncekiSkor { get; set; }
        public string SkorRenk { get; set; }
        //public string sSkorRenk { get; set; }
        public string OncekiSkorRenk { get; set; }

        public string Etki
        {
            get
            {
                var result = Math.Round(this.EtkiPuan ?? 0, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.EtkiSkorTanim);
            }
        }

        public string Olasilik
        {
            get
            {
                var result = Math.Round(this.OlasilikPuan ?? 0, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.OlasilikSkorTanim);
            }
        }
        public string Skor
        {
            get
            {
                var result = Math.Round(this.SkorPuan ?? 0, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.SkorTanim);
            }
        }
    }

    [Serializable]
    public class RiskReportDataSource
    {
        private string dataMember;

        public string DataMember
        {
            get
            {
                return dataMember;
            }
            set
            {
                dataMember = value;
            }
        }
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string dataSourceId;

        public string DataSourceId
        {
            get
            {
                return dataSourceId;
            }
            set
            {
                dataSourceId = value;
            }
        }

        private object value;

        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }




    }

    [Serializable]
    public class RiskReportParameter
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string valueStr;

        public string Value
        {
            get
            {
                return valueStr;
            }
            set
            {
                valueStr = value;
            }
        }


    }

    [Serializable, ComVisible(false)]
    public class RiskReportDataSourceCollection : Collection<RiskReportDataSource>
    {
        public RiskReportDataSourceCollection()
        {
        }
    }


    [Serializable, ComVisible(false)]
    public class RiskReportParameterCollection : Collection<RiskReportParameter>
    {
    }
    
}
