using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Business.Common;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public partial class AdvancedFiltering : EditorViewControl<RiskBusiness>
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                RiskScoreBusiness rsb = new RiskScoreBusiness();
                RiskMatrisBusiness rmb = new RiskMatrisBusiness();
                dsArtikSkorCombo.DataSource = rmb.RetreiveItems();
                dsArtikSkorCombo.DataBind();
                dsArtikEtkiSkorCombo.DataSource = rsb.RetreiveItems();
                dsArtikEtkiSkorCombo.DataBind();
                dsArtikOlasilikSkorCombo.DataSource = rsb.RetreiveItems();
                dsArtikOlasilikSkorCombo.DataBind();
            }
        }

        public RiskFilter Filter
        {
            get
            {
                if (ViewState["Filter"] == null) ViewState["Filter"] = Activator.CreateInstance<RiskFilter>();
                return (RiskFilter)ViewState["Filter"];
            }
            set
            {
                ViewState["Filter"] = value;
            }
        }

        [CommandHandler(CommandName = "ShowAdvancedFiltering")]
        public void ShowAdvancedFilteringCommandHandler(object sender, CommandInfo cmd)
        {
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "DoAdvanceFiltering")]
        public void DoAdvanceFiltering(object sender, CommandInfo cmd)
        {
            var btn = (TTCmdButon)sender;
            if (btn.ID != "ctlAdvanceFilteringClearBtn")
            {

                if (ctlArtikSkorCombo.SelectedItems.Any())
                {
                    foreach (var item in ctlArtikSkorCombo.SelectedItems)
                    {
                        Filter.ArtikSkors.Add(Convert.ToInt32(item.Value));
                    }
                }

                if (ctlArtikEtkiSkorCombo.SelectedItems.Any())
                {
                    foreach (var item in ctlArtikEtkiSkorCombo.SelectedItems)
                    {
                        Filter.ArtikEtkiSkors.Add(Convert.ToInt32(item.Value));
                    }
                }
                if (ctlArtikOlasilikSkorCombo.SelectedItems.Any())
                {
                    foreach (var item in ctlArtikOlasilikSkorCombo.SelectedItems)
                    {
                        Filter.ArtikOlasilikSkors.Add(Convert.ToInt32(item.Value));
                    }
                }
                if (ctlDurumCombo.SelectedItem.Value != null)
                {
                    Filter.RiskDurum = (RiskDurum)Enum.Parse(typeof(RiskDurum), ctlDurumCombo.SelectedItem.Value);
                }
            }
            else
            {
                Filter = new RiskFilter();
                ctlForm.ClearFields();
            }

            ((_default)Page).getLister().BindFilter(Filter);
        }

        internal void Bind(RiskFilter filter)
        {
            if (filter.ArtikSkors.Any())
                ctlArtikSkorCombo.SelectedItems.AddRange(filter.ArtikSkors.Select(p => new SelectedListItem(p.ToString())).AsEnumerable());

            if (filter.ArtikEtkiSkors.Any())
                ctlArtikEtkiSkorCombo.SelectedItems.AddRange(filter.ArtikEtkiSkors.Select(p => new SelectedListItem(p.ToString())).AsEnumerable());
            
            if (filter.ArtikOlasilikSkors.Any())
                ctlArtikOlasilikSkorCombo.SelectedItems.AddRange(filter.ArtikOlasilikSkors.Select(p => new SelectedListItem(p.ToString())).AsEnumerable());
            
            if (filter.RiskDurum != null)
                ctlDurumCombo.SelectedAsString = filter.RiskDurum.ToString();
        }
    }
}