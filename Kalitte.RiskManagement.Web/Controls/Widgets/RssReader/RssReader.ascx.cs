/*  ----------------------------------------------------------------------------
 *  Kalitte Professional Information Technologies
 *  ----------------------------------------------------------------------------
 *  Dynamic Dashboards
 *  ----------------------------------------------------------------------------
 *  File:       RssReader.ascx.cs
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.Dashboard.Framework;


namespace Kalitte.WidgetLibrary.RssReader
{

    public partial class RssReader : System.Web.UI.UserControl, IWidgetControl
    {
        private bool sd = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected bool IsMaximized
        {
            get
            {
                return ViewState["max"] != null;
            }
            set
            {
                if (value)
                    ViewState["max"] = value;
                else
                {
                    ViewState["max"] = null;
                }
            }
        }


        public void Bind(WidgetInstance instance)
        {
            Bind(instance, false);
        }


        public void Bind(WidgetInstance instance, bool DoBind)
        {
            ViewState["Instance"] = instance.InstanceKey;
            ViewState["MaxChar"] = instance.WidgetSettings.ContainsKey("MaxChar") ? instance.WidgetSettings["MaxChar"] : null;
            if (instance.WidgetSettings.ContainsKey("RssUrl"))
            {
                try
                {
                    if (DoBind)
                    {
                        ctlRep.DataSource = null;
                        sd = IsMaximized || (bool)instance.WidgetSettings["ShowBody"];
                        RssToolkit.Rss.RssDocument rss = RssToolkit.Rss.RssDocument.Load(new System.Uri(instance.WidgetSettings["RssUrl"].ToString()));
                        int itemCount = int.Parse(instance.WidgetSettings["Interval"].ToString());
                        if (IsMaximized) itemCount = itemCount * 2;
                        ctlRep.DataSource = rss.SelectItems(itemCount);
                        if ((bool)instance.WidgetSettings["ShowImg"] && rss.Channel.Image != null)
                        {
                            rssImg.Visible = true;
                            rssImg.ImageUrl = rss.Channel.Image.Url;
                        }
                        else rssImg.Visible = false;
                        ctlRep.DataBind();
                        Label1.Text = "";
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = 0;
                        ViewState["Instance"] = instance.InstanceKey;
                        waitTimer.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            waitTimer.Enabled = false;
            MultiView1.ActiveViewIndex = 1;
            Bind(DashboardFramework.GetWidgetInstance(ViewState["Instance"]), true);

        }



        public UpdatePanel[] Command(WidgetInstance instance, WidgetCommandInfo cmd, ref UpdateMode updateMode)
        {
            switch (cmd.CommandType)
            {
                case WidgetCommandType.Refresh:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { RSSUpdatePanel };
                    }
                case WidgetCommandType.SettingsChanged:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { RSSUpdatePanel };
                    }
                case WidgetCommandType.Restored:
                    {
                        IsMaximized = false;
                        Bind(instance);
                        return new UpdatePanel[] { RSSUpdatePanel };
                    }
                case WidgetCommandType.Maximized:
                    {
                        IsMaximized = true;
                        Bind(instance);
                        return new UpdatePanel[] { RSSUpdatePanel };
                    }
                default: return null;
            }
        }





        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Literal l = e.Item.FindControl("description") as Literal;
            if (l != null)
            {
                if (!sd)
                {
                    return;
                }

                string v = DataBinder.Eval(e.Item.DataItem, "description") as string;

                if (ViewState["MaxChar"] != null && !IsMaximized)
                {
                    int chars;
                    if (int.TryParse((string)ViewState["MaxChar"], out chars))
                    {
                        if (v.Length > chars && chars > 0)
                            l.Text = v.Substring(0, chars) + "...";
                        else l.Text = v;
                    }
                    else l.Text = v;
                }
                else l.Text = v;
            }
        }






        public void InitControl(WidgetInitParameters parameters)
        {

        }


    }
}
