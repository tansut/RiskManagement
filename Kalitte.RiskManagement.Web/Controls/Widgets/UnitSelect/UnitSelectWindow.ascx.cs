using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Core;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.Dashboard.Framework;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.UnitSelect
{


    public partial class UnitSelectWindow : System.Web.UI.UserControl, IUnitSelector, IWindowControl, ISupportsMultiplePostbackTypes
    {
        public PostbackType PostbackType { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string WindowClientID
        {
            get
            {
                return ctlUnitSelectWindow.ClientID;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ctlSelector.OnFilter += new EventHandler(ctlSelector_OnFilter);
            switch (PostbackType)
            {
                case PostbackType.AspNet:
                    ctlFilter.DirectEvents.Click.Clear();
                    ctlFilter.AutoPostBack = true;
                    ctlFilter.Click += new EventHandler(ctlFilter_Click);
                    ctlCancelFilter.DirectEvents.Click.Clear();
                    ctlCancelFilter.AutoPostBack = true;
                    ctlCancelFilter.Click += new EventHandler(ctlCancelFilter_Click);
                    break;
                case PostbackType.ExtNet:
                    ctlFilter.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(filterHandler);
                    ctlCancelFilter.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(cancelfilterHandler);

                    break;
            }
        }

        void ctlCancelFilter_Click(object sender, EventArgs e)
        {
            UnitFilterManager.ClearActiveUnits();
            doFilter();
        }

        void ctlFilter_Click(object sender, EventArgs e)
        {
            var bll = new UnitBusiness();
            UnitFilterManager.SetActiveUnits(ctlSelector.UpdatedUnits);
            doFilter();
        }

        protected void filterHandler(object sender, DirectEventArgs e)
        {
            var bll = new UnitBusiness();
            UnitFilterManager.SetActiveUnits(ctlSelector.UpdatedUnits);
            doFilter();
        }

        void ctlSelector_OnFilter(object sender, EventArgs e)
        {
            doFilter();
        }

        internal void doFilter()
        {
            if (OnFilter != null)
                OnFilter(this, EventArgs.Empty);
        }

        protected void cancelfilterHandler(object sender, DirectEventArgs e)
        {
            UnitFilterManager.ClearActiveUnits();
            doFilter();
        }

        #region IUnitSelector Members

        public event EventHandler OnFilter;



        public void Bind()
        {
            ctlSelector.Bind();

        }

        #endregion
    }
}