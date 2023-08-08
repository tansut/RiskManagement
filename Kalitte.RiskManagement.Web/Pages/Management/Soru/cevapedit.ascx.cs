using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Controls;
using System.Web.Security;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.Soru
{

    public partial class cevapedit : EditorViewControl<AnswerBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            var type = (SoruGrupTur)command.Parameters["type"];
            ctlPuan.Tur = type;
            ctlPuan.DataBind();
            entityWindow.Title = "Cevap Tanımla";
            ctlSave.CommandName = "CreateAnswer";
            ctlTabs.ActiveTabIndex = 0;
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = command.Parameters["entity"] as Cevap;
            var type = (SoruGrupTur)command.Parameters["type"];
            ctlPuan.Tur = type;
            ctlPuan.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = "UpdateAnswer";
            CurrentID = entity.ID;
            ctlAd.Text = entity.Ad;
            ctlAciklama.Text = entity.Yardim;
            ctlPuan.Puan = entity.Puan;
            entityWindow.Show();
        }

        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = command.Parameters["entity"] as Cevap;
            entity.Ad = ctlAd.Text;
            entity.Yardim = ctlAciklama.Text;
            entity.Puan = ctlPuan.Puan;
            entityWindow.Hide();
        }

        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = command.Parameters["entity"] as Cevap;
            entity.Ad = ctlAd.Text;
            entity.Yardim = ctlAciklama.Text;
            entity.Puan = ctlPuan.Puan;
            entityWindow.Hide();
        }

    }
}