/*  ----------------------------------------------------------------------------
 *  Kalitte Professional Information Technologies
 *  ----------------------------------------------------------------------------
 *  Dynamic Dashboards
 *  ----------------------------------------------------------------------------
 *  File:       BaseChart.ascx.cs
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using Kalitte.Dashboard.Framework.Types;
using System.Collections;
using Kalitte.Dashboard.Framework;
using System.Data;
using System.Threading;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Common;


namespace Kalitte.WidgetLibrary.Charting
{
    public class SimpleChartDataEntity
    {
        public SimpleChartDataEntity()
        {
        }

        public SimpleChartDataEntity(string x, double y)
        {
            XTitle = x;
            Y = y;
        }

        public SimpleChartDataEntity(double x, double y)
        {
            X = x;
            Y = y;
        }

        public string XTitle { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

    }

    public abstract partial class BaseChart : System.Web.UI.UserControl, IWidgetControl
    {
        protected DashboardSurface surface;
        protected WidgetInstance instance;

        private const string FUNNELGAPWIDTH = "3";
        private const string POINTWIDTH = "0.8";
        private Color SERIESBORDERCOLOR = Color.FromArgb(180, 26, 59, 105);

        protected abstract void SetSettings(ChartSettings settings);
        protected abstract void DataBindChart(WidgetInstance instance);
        protected abstract Chart ThisChart { get; }
        protected abstract UpdatePanel ThisUpdatePanel { get; }

        private List<RiskMatrisTanim> riskMatrisTanim = null;
        private List<RiskMatrisEntity> riskMatris = null;



        protected List<RiskMatrisEntity> RiskMatris
        {
            get
            {
                if (riskMatris == null)
                    riskMatris = new RiskMatrisBusiness().GetMatrixAsList();
                return riskMatris;
            }
        }

        protected List<RiskMatrisTanim> RiskMatrisTanimList
        {
            get
            {
                if (riskMatrisTanim == null)
                    riskMatrisTanim = new RiskMatrisBusiness().RetreiveEntityItems();
                return riskMatrisTanim;
            }
        }


        protected virtual void SetPointColors()
        {
            foreach (var serie in ThisChart.Series)
            {
                SetPointColors(serie);
            }
        }

        protected virtual void SetPointColorsByValue()
        {
            foreach (var serie in ThisChart.Series)
            {
                SetPointColorsByValue(serie);
            }
        }

        protected virtual void SetPointColorsByLegendText()
        {
            foreach (var serie in ThisChart.Series)
            {
                SetPointColorsByLegendText(serie);
            }
        }

        protected virtual void SetPointColorsByValue(Series serie)
        {
            foreach (var p in serie.Points)
            {
                var itemScore = RiskMatrisBusiness.GetMatixStore(RiskMatris, Convert.ToInt32(p.YValues[0]));
                if (itemScore != null)
                {
                    p.Color = ColorTranslator.FromHtml(itemScore.Color);
                    serie.Color = p.Color;
                }
            }
        }

        protected virtual void SetPointColorsByLegendText(Series serie)
        {
            foreach (var p in serie.Points)
            {
                var itemScore = RiskMatrisTanimList.SingleOrDefault(o => o.GrupDeger == p.LegendText);
                if (itemScore != null)
                {
                    p.Color = ColorTranslator.FromHtml(itemScore.Renk);
                    serie.Color = p.Color;
                }
            }
        }

        protected virtual void SetPointColors(Series serie)
        {
            foreach (var p in serie.Points)
            {
                var itemScore = RiskMatrisTanimList.SingleOrDefault(o => o.GrupDeger == p.AxisLabel);
                if (itemScore != null)
                    p.Color = ColorTranslator.FromHtml(itemScore.Renk);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ThisChart.PrePaint += new EventHandler<ChartPaintEventArgs>(chr_PrePaint);
        }

        public void InitControl(WidgetInitParameters parameters)
        {
            surface = parameters.Surface;
            instance = parameters.Instance;
            surface.DashboardContentsUpdated += new EventHandler(surface_DashboardContentsUpdated);


        }

        void surface_DashboardContentsUpdated(object sender, EventArgs e)
        {
            if (Page.IsPostBack && ViewState["instance"] != null)
            {
                WidgetInstance instance = DashboardFramework.GetWidgetInstance(ViewState["instance"]);
                if (instance != null)
                    LoadChart(instance);
            }
        }


        void chr_PrePaint(object sender, ChartPaintEventArgs e)
        {
            if (this.PageChartSettings != null)
            {
                foreach (var item in ThisChart.Series)
                {
                    item.Color = Color.FromArgb(this.PageChartSettings.AlphaLevel == 0 ? 255 : this.PageChartSettings.AlphaLevel, item.Color);
                }
            }
        }

        protected virtual bool ShowChart(WidgetInstance instance)
        {
            ChartSettings settings = instance.SerializedData as ChartSettings;
            if (settings == null)
                return false;
            else return true;
        }


        protected virtual ChartSettings GetSettings(WidgetInstance instance)
        {
            return instance.SerializedData as ChartSettings;
        }


        public object GetSettingsAsObject(WidgetInstance instance)
        {
            return instance.SerializedData;
        }

        protected virtual void LoadChart(WidgetInstance instance)
        {
            if (!ShowChart(instance))
            {
                SetSettings(instance.UserData as ChartSettings);
                ThisChart.Visible = false;
            }
            else
            {
                ChartSettings settings = GetSettings(instance);
                SetSettings(settings);

                this.PageChartSettings = settings;

                SettingPreRenderProperties(ThisChart, settings);

                DataBindChart(instance);

                foreach (Series series in ThisChart.Series)
                {
                    SettingCommonSeriesProperties(series, settings);
                }

                SetCommonChartProperties(ThisChart, settings);

                ThisChart.Visible = true;
            }
        }



        protected virtual void SettingCommonSeriesProperties(Series series, ChartSettings settings)
        {
            series.ChartType = (SeriesChartType)settings.ChartType;
            series.IsValueShownAsLabel = settings.IsShownValues;

            series.MarkerStyle = (MarkerStyle)Enum.Parse(typeof(MarkerStyle), string.IsNullOrEmpty(settings.MarkerStyle) ? "None" : settings.MarkerStyle.ToString());
            series.MarkerSize = settings.MarkerSize;

            if ((settings.ChartType == (int)SeriesChartType.Pie)
                || (settings.ChartType == (int)SeriesChartType.Doughnut)
                || (settings.ChartType == (int)SeriesChartType.Funnel))
            {
                series["PieLabelStyle"] = settings.PieLabelStyle;
                series["PieDrawingStyle"] = settings.PieDrawingStyle;

                if (settings.CollectPieOther)
                {
                    series["CollectedLabel"] = "Diğerleri";
                    series["CollectedLegendText"] = "Diğerleri";
                    series["CollectedThreshold"] = settings.CollectedPieTreshold.ToString();
                    series["CollectedColor"] = Color.Gold.Name;
                }

                if (!series.IsValueShownAsLabel && settings.ShowLabels)

                    foreach (var item in series.Points)
                    {
                        item.Label = string.Format("{0} ({1})", item.LegendText, item.Label);
                    }
            }

            if ((settings.ChartType == (int)SeriesChartType.Funnel))
            {
                series["FunnelPointGap"] = FUNNELGAPWIDTH;
            }

            if ((settings.ChartType == (int)SeriesChartType.Bar)
                || (settings.ChartType == (int)SeriesChartType.Column))
            {
                series["PointWidth"] = POINTWIDTH;
            }

            series.Font = new Font("Trebuchet MS", 8, FontStyle.Bold);
          
            series.LabelFormat = "N0";
            //series["DrawingStyle"] = "Cylinder";
            series["ShowMarkerLines"] = settings.ShowMarkersLine.ToString();
            series.ShadowColor = Color.Transparent;
            if (settings.ChartType == (int)SeriesChartType.Line
                || settings.ChartType == (int)SeriesChartType.Spline
            || settings.ChartType == (int)SeriesChartType.StepLine)
                series.BorderWidth = 4;

            

            series.ToolTip = String.Format("{0}: #VALX=#VALY", series.Name);

            series.BorderColor = settings.HasBorderColor ? SERIESBORDERCOLOR : Color.Empty;



        }

        protected virtual void SettingPreRenderProperties(Chart chart, ChartSettings settings)
        {

        }



        protected virtual void SetCommonChartProperties(Chart chart, ChartSettings chartSettings)
        {
            foreach (var item in chart.ChartAreas)
            {
                item.AxisX.IsLabelAutoFit = true;
                item.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.LabelsAngleStep90;
                item.BackColor = Color.Transparent;
                item.BackSecondaryColor = Color.Transparent;
                item.ShadowColor = Color.Transparent;
                item.Area3DStyle.Enable3D = chartSettings.Enable3D;
                item.Area3DStyle.IsClustered = !chartSettings.IsClustured;
                item.AxisY.IsLabelAutoFit = true;
                item.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
            }


            if (chart.Titles.Count > 0 && !string.IsNullOrWhiteSpace(chartSettings.Title))
            {
                var titles = chartSettings.Title.Split(',');
                for (int i = 0; i < chart.Titles.Count; i++)
                {
                    string title = i < titles.Length ? titles[i] : "";
                    if (string.IsNullOrWhiteSpace(title))
                        chart.Titles[i].Visible = false;
                    else chart.Titles[i].Text = title;
                    chart.Titles[i].Alignment = (ContentAlignment)Enum.Parse(typeof(ContentAlignment), string.IsNullOrEmpty(chartSettings.TitleAlignment) ? "2" : chartSettings.TitleAlignment);

                }
            }
            else chart.Titles.Clear();

            foreach (var item in chart.Legends)
            {
                item.Enabled = chartSettings.LegendsEnabled;

                if (chartSettings.LegendsEnabled)
                {
                    item.Alignment = (StringAlignment)Enum.Parse(typeof(StringAlignment), string.IsNullOrEmpty(chartSettings.LegendAlignment) ? "1" : chartSettings.LegendAlignment);
                    item.LegendStyle = (LegendStyle)Enum.Parse(typeof(LegendStyle), string.IsNullOrEmpty(chartSettings.LegendStyle) ? "0" : chartSettings.LegendStyle);
                    item.Docking = (Docking)Enum.Parse(typeof(Docking), string.IsNullOrEmpty(chartSettings.LegendDocking) ? "2" : chartSettings.LegendDocking);
                }
            }




            double ratio = 1;

            if (IsMaximized)
            {
                ratio = Convert.ToDouble(MaximizedWidth) / chartSettings.Width;
                if (MaximizedHeight < chartSettings.Height * ratio)
                    ratio = Convert.ToDouble(MaximizedHeight) / chartSettings.Height;
            }

            chart.Width = Unit.Pixel(Convert.ToInt32(chartSettings.Width * ratio));
            chart.Height = Unit.Pixel(Convert.ToInt32(chartSettings.Height * ratio));

            chart.BackColor = Color.FromName(string.IsNullOrEmpty(chartSettings.BackColor) ? "White" : chartSettings.BackColor);
            chart.BackSecondaryColor = Color.FromName(string.IsNullOrEmpty(chartSettings.SecondaryBackColor) ? "White" : chartSettings.SecondaryBackColor);

            chart.BackGradientStyle = (GradientStyle)Enum.Parse(typeof(GradientStyle), string.IsNullOrEmpty(chartSettings.Gradient) ? "None" : chartSettings.Gradient);
            chart.BorderSkin.SkinStyle = (BorderSkinStyle)Enum.Parse(typeof(BorderSkinStyle), string.IsNullOrEmpty(chartSettings.BorderSkin) ? "None" : chartSettings.BorderSkin);
            chart.Palette = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), string.IsNullOrEmpty(chartSettings.Palette) ? "None" : chartSettings.Palette);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Page.IsPostBack && ViewState["instance"] != null)
            //{
            //    WidgetInstance instance = DashboardFramework.GetWidgetInstance(ViewState["instance"]);
            //    if (instance != null)
            //        LoadChart(instance);
            //}
        }



        public void Bind(WidgetInstance instance)
        {
            LoadChart(instance);
            ViewState["instance"] = instance.InstanceKey;
        }






        private ChartSettings PageChartSettings { get; set; }





        public UpdatePanel[] Command(WidgetInstance instance, Kalitte.Dashboard.Framework.WidgetCommandInfo commandData, ref UpdateMode updateMode)
        {
            if (commandData.CommandType == WidgetCommandType.Refresh || commandData.CommandType == WidgetCommandType.SettingsChanged)
            {
                Bind(instance);
                return new UpdatePanel[] { ThisUpdatePanel };
            }
            else if (commandData.CommandType == WidgetCommandType.Maximized)
            {
                IsMaximized = true;
                MaximizedWidth = int.Parse(commandData.Arguments["width"].ToString());
                MaximizedHeight = int.Parse(commandData.Arguments["height"].ToString());
                Bind(instance);
                return new UpdatePanel[] { ThisUpdatePanel };
            }
            else if (commandData.CommandType == WidgetCommandType.Restored)
            {
                IsMaximized = false;
                Bind(instance);
                return new UpdatePanel[] { ThisUpdatePanel };
            }
            else return null;
        }

        protected virtual int MaxWidthOffset
        {
            get
            {
                return 0;
            }
        }

        protected virtual int MaxHeightOffset
        {
            get
            {
                return 0;
            }
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
                    MaximizedHeight = 0;
                    MaximizedWidth = 0;
                }
            }
        }

        protected int MaximizedWidth
        {
            get
            {
                return ViewState["MaximizedWidth"] == null ? 0 : (int)ViewState["MaximizedWidth"];
            }
            set
            {
                if (value == 0)
                    ViewState["MaximizedWidth"] = null;
                else ViewState["MaximizedWidth"] = value;
            }
        }

        protected int MaximizedHeight
        {
            get
            {
                return ViewState["MaximizedHeight"] == null ? 0 : (int)ViewState["MaximizedHeight"];
            }
            set
            {
                if (value == 0)
                    ViewState["MaximizedHeight"] = null;
                else ViewState["MaximizedHeight"] = value;
            }
        }


    }
}
