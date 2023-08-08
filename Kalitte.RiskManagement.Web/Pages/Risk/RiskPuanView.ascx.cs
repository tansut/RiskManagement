using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Business.Common;
using System.Threading;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public partial class RiskPuanView : EditorViewControl<RiskBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.Net.X.IsAjaxRequest)
            {

                ctlRiskMatrix.Store.Primary.DataSource = new TTRiskScoreStore().GetRows();
                ctlRiskMatrix.Store.Primary.DataBind();

            }
        }

        [CommandHandler(CommandName = "ShowPuanView")]
        protected void ShowPuanViewHandler(object sender, CommandInfo e)
        {
            var entity = BusinessObject.Retrieve(e.RecordID);
            if (entity.RiskDurum == RiskDurum.PuanlamaBekler || entity.RiskDurum == RiskDurum.OnayBekler)
            {
                ctlRiskNote.Text = "Risk henüz onaylanmamış durumdadır. Aşağıdaki değerler çalışanların şu ana kadar yaptıkları puanlama neticesinde hesaplanmıştır.";
                ctlRiskNote.Icon = Ext.Net.Icon.Exclamation;
                BusinessObject.CalculateRiskPuan(entity);
                ctlApproveBtn.Show();
            }
            else
            {
                ctlRiskNote.Text = entity.Ad;
                ctlRiskNote.Icon = Ext.Net.Icon.Accept;
                ctlApproveBtn.Hide();
                BusinessObject.CalculateSkorVisualization(entity);
            }

            ctlArtikRiskPuanLabel.Html = string.Format("<h2>{0} - {1}</h2>", Math.Round(entity.ArtikSkor ?? 0, 2), entity.ArtikSkorTanim);
            ctlArtikRiskPanel.BodyStyle = string.Format("text-align:center;background-color: {0}", entity.ArtikSkorRenk);
            ctlArtikRiskEtkiLabel.Html = string.Format("<h3>{0} - {1}</h3>", Math.Round(entity.ArtikEtki ?? 0, 2), entity.ArtikEtkiSkorTanim);
            ctlArtikRiskOlasilikLabel.Html = string.Format("<h3>{0} - {1}</h3>", Math.Round(entity.ArtikOlasilik??0,2), entity.ArtikOlasilikSkorTanim);

            CurrentID = entity.ID;
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "ApproveRisk")]
        protected void ApproveRiskCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);

            BusinessObject.ChangeRiskStatus(entity, RiskDurum.Onaylandı);
            entityWindow.Hide();
            PageInstance.GetLister<RiskBusiness>().LoadItems();
        }
    }
}