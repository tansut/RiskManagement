using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

using System.Collections;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.UI
{

    public enum ViewControlType
    {
        Lister,
        Editor
    }


    public abstract class BaseViewUserControl: UserControl
    {

        public abstract ViewControlType ControlType { get; }

        public virtual string ControlID
        {
            get
            {
                return string.Empty;
            }
        }

        protected int CurrentID
        {
            get
            {
                if (ViewState["ci"] == null)
                    return -1;
                else return (int)ViewState["ci"];
            }
            set
            {
                ViewState["ci"] = value;
            }
        }

        protected int CurrentDetailID
        {
            get
            {
                if (ViewState["cdi"] == null)
                    return -1;
                else return (int)ViewState["cdi"];
            }
            set
            {
                ViewState["cdi"] = value;
            }
        }

        protected int UserID
        {
            get
            {
                if (ViewState["uid"] == null)
                    return -1;
                else return (int)ViewState["uid"];
            }
            set
            {
                ViewState["uid"] = value;
            }
        }

        protected string State
        {
            get
            {
                if (ViewState["cis"] == null)
                    return string.Empty;
                else return ViewState["cis"].ToString();
            }
            set
            {
                ViewState["cis"] = value;
            }
        }


        protected ViewPageBase PageInstance
        {
            get
            {
                return (ViewPageBase)this.Page;
            }
        }

        public virtual void Show(object sender, CommandInfo command)
        {

        }
    }
}
