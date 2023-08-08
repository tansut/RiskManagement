using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Reporting;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class BirimOzetList : BaseDataList
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BindData(instance);

        }


        protected override void BindData(Dashboard.Framework.Types.WidgetInstance instance)
        {

            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();
            UnitReportingBusiness urb = new UnitReportingBusiness();
            var surec = urb.GetSurecCount(listingParams);
            var risk = urb.GetRiskCount(listingParams);
            var onaylananrisk = urb.GetApprovedRiskCount(listingParams);
            var puanlamabeklerrisk = urb.GetNotAppovedRiskCount(listingParams);
            var onaybeklerrisk = urb.GetWaitingApproveRiskCount(listingParams);
            var kullanici = urb.GetUserCount(listingParams);
            var kontrol = urb.GetKontrolCount(listingParams);
            var ilkPuan = urb.GetIlkPuanDate(listingParams);
            var sonPuan = urb.GetSonPuanDate(listingParams);
            ctlKullaniciSayisi.Text = kullanici.ToString();
            if (kontrol != 0)
            {
                ctlRiskKontrolOran.Text = ((double)kontrol / (double)risk).ToString("0.0000");
                ctlSurecKontrolOran.Text = ((double)kontrol / (double)surec).ToString("0.0000");
            }
            else
            {
                ctlRiskKontrolOran.Text = "-";
                ctlSurecKontrolOran.Text = "-";
            }
            if (kullanici != 0)
            {
                ctlSurec.Text = surec.ToString();
                ctlOnaylananRisk.Text = onaylananrisk.ToString();
                ctlPuanlamaBeklerRisk.Text = puanlamabeklerrisk.ToString();
                ctlOnayBeklerRisk.Text = onaybeklerrisk.ToString();
                ctlToplamRisk.Text = risk.ToString();
                ctlKontrol.Text = kontrol.ToString();
            }
            else
            {
                ctlSurec.Text = "-";
                ctlOnaylananRisk.Text = "-";
                ctlPuanlamaBeklerRisk.Text = "-";
                ctlKontrol.Text = "-";
            }
            ctlSurecRiskOran.Text = ((double)risk / (double)surec).ToString("0.00");
            ctlSonPuanlama.Text = !sonPuan.HasValue ? "-" : sonPuan.Value.ToString("dd.MM.yyyy");
            ctlEtki.Text = urb.GetAverageRiskEtki(listingParams).ToString();
            ctlOlasilik.Text = urb.GetAverageRiskOlasilik(listingParams).ToString();
            var skor = urb.GetAverageRiskSkor(listingParams);
            var matrix = urb.GetAverageRiskSkorColor((int)skor);
            ctlSkor.Text = string.Format("{0} \\ {1}", skor.ToString(), matrix.Display);
            ctlSkorContainer.Attributes.Add("style", string.Format("background-color:{0}", matrix.Color));
        }


        protected override UpdatePanel ThisUpdatePanel
        {
            get { return this.up; }
        }
    }
}