using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Model.Common;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.BI.Shared;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public partial class list : ListerViewControl<RiskBusiness>
    {
        Dictionary<string, string> groupUsers;

        private Dictionary<string, string> UserGroupUsers
        {
            get
            {
                if (groupUsers == null)
                {
                    var items = new CalismaGrupBusiness().GetAllGroupUsersOfCurrentUser(true);
                    groupUsers = new Dictionary<string, string>(items.Count);
                    items.ForEach(p => groupUsers.Add(p.UserName, p.AdSoyad));
                }
                return groupUsers;
            }
        }

        protected override TTStore LocateListerStore()
        {
            return this.dsMain;
        }

        protected override TTGrid LocateGrid()
        {
            return this.grid;
        }

        private IUnitSelector unitSelect;

        private void createUnitWindow()
        {
            Control unitSelector = LoadControl("~/Controls/Widgets/UnitSelect/UnitSelectWindow.ascx");
            unitSelect = unitSelector as IUnitSelector;
            unitSelector.ID = this.ID + "selector";
            (unitSelector as ISupportsMultiplePostbackTypes).PostbackType = PostbackType.ExtNet;
            unitSelect.OnFilter += new EventHandler(unitSelect_OnFilter);
        }

        void unitSelect_OnFilter(object sender, EventArgs e)
        {
            LoadItems();
        }

        public void BindFilter(RiskFilter rf)
        {
            Filter = rf;
            LoadItems();
        }

        [CommandHandler(CommandName = "ShowRiskRecordReport")]
        public void RiskRecordReportHandler(object sender, CommandInfo command)
        {
            var lp = this.GetListingParameters();
            var list = BusinessObject.RetreiveItemsWithControls(lp);
            var reportData = new ReportData();
            reportData.AddDataSource("Kontrol", list);
            ctlReportWindow.Execute("RISKKAYDIRAPORU", reportData);
        }

        [CommandHandler(CommandName = "ShowControlReport")]
        public void ControlReportHandler(object sender, CommandInfo command)
        {
            var lp = this.GetListingParameters();
            var list = BusinessObject.RetreiveItemsWithControls(lp, false);
            var reportData = new ReportData();
            reportData.AddDataSource("Kontrol", list);
            ctlReportWindow.Execute("KONTROLLISTESIRAPORU", reportData);
        }

        [CommandHandler(CommandName = "ShowHistoryReport")]
        public void HistoryReportHandler(object sender, CommandInfo command)
        {

            var lp = this.GetListingParameters();
            var list = new RiskGecmisBusiness().RetreiveHistoryItems(lp);
            var reportData = new ReportData();
            reportData.AddDataSource("DataSet1", list);
            ctlReportWindow.Execute("GECMISLISTESIRAPORU", reportData);
        }

        [CommandHandler(KnownCommand = KnownCommand.ViewReport)]
        public void ReportHandler(object sender, CommandInfo command)
        {
            var list = this.GetItems();
            var reportData = new ReportData();
            reportData.AddDataSource("Risk", list);
            ctlReportWindow.Execute("RISKLISTESIRAPORU", reportData);
        }

        [CommandHandler(CommandName = "ShowStatusScoreReport")]
        public void StatusReportHandler(object sender, CommandInfo command)
        {
            var list = this.GetItems();
            var reportData = new ReportData();
            reportData.AddDataSource("Risk", list);
            ctlReportWindow.Execute("RISKDURUMSKORRAPORU", reportData);
        }

        [CommandHandler(CommandName = "CalismaGrubuGoster")]
        public void CalismaGrubuGosterHandler(object sender, CommandInfo cmd)
        {
            ((_default)Page).getCalismaGrubuList().Bind(cmd.RecordID);
        }

        [CommandHandler(CommandName = "ResetRiskScores")]
        public void ResetRiskScores(object sender, CommandInfo cmd)
        {
            throw new BusinessException("Bu işlev geçici olarak hizmet dışıdır!");
            //BusinessObject.ResetRiskScore();
            //LoadItems();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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

        public HashSet<int> GetUnitsFromString(string units)
        {
            HashSet<int> result = new HashSet<int>();
            var array = units.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array)
            {
                result.Add(Convert.ToInt32(item));
            }
            return result;
        }

        protected override ListingParameters GetListingParameters()
        {
            var lp = base.GetListingParameters();

            lp.UserParams.Add("StatusFilter", true);

            if (!string.IsNullOrWhiteSpace(Request["selectDataKey"]))
            {
                var filter = Session["SelectData-" + Request["selectDataKey"]] as FilterSelectData;
                Session["SelectData-" + Request["selectDataKey"]] = null;
                lp.UserParams.Clear();
                if (filter.IDList != null)
                {
                    lp.UserParams.Add("idFilter", filter.IDList);
                }
                else
                    lp.UserParams.Add("idFilter", new int[1] { filter.ID });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Request["Birims"]))
                {
                    UnitFilterManager.SetActiveUnits(GetUnitsFromString(Request["Birims"]));
                }

                lp.Units = UnitFilterManager.GetActiveUnits();
                if (!string.IsNullOrWhiteSpace(Request["filter"]))
                {
                    var filter = RiskFilter.FromString(HttpUtility.UrlDecode(Request["filter"]));
                    ((_default)Page).getAdvanceFiltering().Bind(filter);
                    lp.UserParams.Add("riskFilter", filter);
                }
            }

            //var skorfilter = prms.FieldFilters.Where(p => p.Name == "SkorFullInfo").SingleOrDefault();
            //var artikskorfilter = prms.FieldFilters.Where(p => p.Name == "ArtikSkorFullInfo").SingleOrDefault();

            //if (skorfilter != null)
            //{
            //    prms.FieldFilters.Remove(skorfilter);
            //    prms.UserParams.Add("SkorFullInfo", skorfilter.Value);
            //}

            //if (artikskorfilter != null)
            //{
            //    prms.FieldFilters.Remove(artikskorfilter);
            //    prms.UserParams.Add("ArtikSkorFullInfo", artikskorfilter.Value);
            //}

            if (Filter.HasItems())
            {
                lp.UserParams.Add("riskFilter", Filter);
            }

            return lp;
        }

        protected override System.Collections.IList GetItems()
        {
            var lp = GetListingParameters();
            //lp.FieldFilters.Add(new FilteringInfo("Durum", RiskDurum.PuanlamaBekler.ToString()));
            //lp.FieldFilters.Add(new FilteringInfo("Durum", RiskDurum.Onaylandı.ToString()));
            //lp.FieldFilters.Operator = FilterListOperator.Or;

            //if (Filter.HasItems())
            //{
            //    lp.UserParams.Add("riskFilter", Filter);
            //}
            var ub = new UserBusiness();
            var user = UserBusiness.GetUser();
            var roles = ub.GetUserRoles(user.UserName);
            List<Kalitte.RiskManagement.Framework.Model.Risk> itemlist;
            if (roles.Any(r => r == RoleConstants.UnitWorker) && !roles.Any(d=>d==RoleConstants.GlobalWorkflowManager))
            {
                itemlist = BusinessObject.GetUnitWorkerRisks(AuthenticationManager.CurrentUserID, lp);
            }
            else
            {
                itemlist = BusinessObject.RetreiveItems(lp) as List<Kalitte.RiskManagement.Framework.Model.Risk>;
                
            }
            var items = itemlist.Where(p => p.Surec.Aktif);


            items=items.OrderBy(p => p.SurecID);
        //    if (ub.GetUserRoles().Any(x => x == RoleConstants.UnitWorker)) ;
            DynamicEntityList list = new DynamicEntityList();
            Dictionary<string, Type> columns = new Dictionary<string, Type>();
            columns.Add("ID", typeof(int));
            columns.Add("SurecAd", typeof(string));
            columns.Add("SurecBirim", typeof(string));
            columns.Add("Ad", typeof(string));
            columns.Add("ArtikSkorFullInfo", typeof(string));
            columns.Add("Durum", typeof(string));
            columns.Add("ArtikSkorRenk", typeof(string));
            columns.Add("ArtikEtkiFullInfo", typeof(string));
            columns.Add("ArtikOlasilikFullInfo", typeof(string));
            columns.Add("RiskKullanici", typeof(string));
            columns.Add("KayitTarih", typeof(string));
            columns.Add("ArtikSkor", typeof(int?));

            foreach (var col in UserGroupUsers)
            {
                columns.Add(string.Format("m{0}", col.Key), typeof(string));
            }

            foreach (var item in items)
            {
                List<object> values = new List<object>();
                values.Add(item.ID);
                values.Add(item.SurecAd);
                values.Add(item.SurecBirim);
                values.Add(item.Ad);
                values.Add(item.ArtikSkorFullInfo);
                values.Add(item.Durum);
                values.Add(item.ArtikSkorRenk);
                values.Add(item.ArtikEtkiFullInfo);
                values.Add(item.ArtikOlasilikFullInfo);
                values.Add(item.RiskKullanici);
                values.Add(item.KayitTarih.ToString("dd/MM/yyyy"));
                values.Add(item.ArtikSkor);

                foreach (var col in UserGroupUsers)
                {
                    values.Add(item.GetUserStatus(col.Key).ToString());
                }
                DynamicEntity entity = new DynamicEntity(columns, values.ToArray());
                list.Add(entity);
            }

            return list;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            createUnitWindow();
            this.Controls.Add(unitSelect as Control);
            ctlUnitFilterBtn.Listeners.Click.Handler = string.Format("Ext.getCmp('{0}').show();", (unitSelect as IWindowControl).WindowClientID);
            foreach (var item in UserGroupUsers)
            {
                JsonReader reader = Store.Reader[0] as JsonReader;
                foreach (var col in UserGroupUsers)
                    reader.Fields.Add(new RecordField(string.Format("m{0}", col.Key)));
            }
            if (!Ext.Net.X.IsAjaxRequest)
            {

                foreach (var col in UserGroupUsers)
                {
                    TTColumn column = new TTColumn();
                    column.Width = new Unit(120);
                    column.DataIndex = string.Format("m{0}", col.Key);
                    column.Header = col.Value;
                    column.AutoFilter = false;
                    column.Sortable = false;
                    column.Renderer = new Renderer() { Fn = "statusCol" };
                    grid.ColumnModel.Columns.Add(column);
                }
            }
        }
    }
}