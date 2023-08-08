<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.OrganisationChart.View" %>
<link type="text/css" rel="stylesheet" href="/Controls/Widgets/OrganisationChart/resources/slickmap.css" />
<asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div runat="server" id="rootTree" class="sitemap">
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
