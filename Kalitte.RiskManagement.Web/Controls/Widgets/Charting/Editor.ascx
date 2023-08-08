<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Editor.ascx.cs" 
Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.Charting.Editor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="ChartSettingsControl.ascx" TagName="ChartSettingsControl"
    TagPrefix="uc1" %>
<cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
    <cc1:TabPanel runat="server" HeaderText="Genel" ID="TabPanel1">
        <ContentTemplate>

        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel runat="server" HeaderText="Grafik Özellikleri" ID="TabPanel2">
        <ContentTemplate>
            <uc1:ChartSettingsControl ID="ctlChartSettings" runat="server" />
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>