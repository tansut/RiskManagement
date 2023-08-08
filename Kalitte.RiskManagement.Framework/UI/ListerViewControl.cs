using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Controls;
using Ext.Net;
using System.Collections;
using Kalitte.RiskManagement.Framework.Business;
using Ext.Net.Utilities;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.UI
{
    public abstract class ListerViewControl<T> : ViewControlBase<T> where T : BusinessBase
    {
        protected TTStore Store { get; private set; }
        protected TTGrid Grid { get; private set; }

        private StoreRefreshDataEventArgs currentRefreshArgs = null;

        public override ViewControlType ControlType
        {
            get { return ViewControlType.Lister; }
        }

        protected virtual ListingParameters GetListingParameters()
        {
            ListingParameters result;
            if (currentRefreshArgs != null)
            {
                result = new ListingParameters(currentRefreshArgs);
                var filters = Grid.Filters;
                if (filters != null)
                {
                    var filter = currentRefreshArgs.Parameters[filters.ParamPrefix];
                    if (!string.IsNullOrEmpty(filter))
                    {
                        FilterConditions conditions = new FilterConditions(filter);
                        result.SetFieldFilters(conditions);
                    }
                }
            }
            else result = new ListingParameters();
            return result;
        }

        public virtual void DataBindStore(IList items)
        {
            Store.DataSource = items;
            Store.DataBind();
            //Grid.ClearSelections();
            //if (items.Count > 0)
            //    Grid.SelectIfNotSelected(0);
        }

        public virtual void LoadItems()
        {
            if (!Store.UseServerSidePaging)
            {
                IList source = GetItems();
                if (source == null)
                    return;
                DataBindStore(source);
            }
            else
                Store.DataBind();
        }

        protected virtual System.Collections.IList GetItems()
        {
            var listingParameters = GetListingParameters();
            return BusinessObject.RetreiveItems(listingParameters);
        }

        public ListerViewControl()
            : base()
        {

        }

        protected virtual TTGrid LocateGrid()
        {
            return ControlUtils.FindControl<TTGrid>(this);
        }

        protected virtual TTStore LocateListerStore()
        {
            return ControlUtils.FindControl<TTStore>(this);

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Store = LocateListerStore();
            Grid = LocateGrid();
            if (Store != null)
            {
                Store.RefreshData += new Ext.Net.Store.AjaxRefreshDataEventHandler(Store_RefreshData);
                Page.Load += new EventHandler(Page_Load);
            }
        }



        void Store_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            currentRefreshArgs = e;
            IList source = GetItems();
            if (source == null)
                return;
            DataBindStore(source);

        }

        void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest && Store.AutoLoad)
            {
                LoadItems();
            }
        }



    }
}
