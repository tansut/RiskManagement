<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DefaultMaster.Master"
    AutoEventWireup="true" CodeBehind="BirimKarne.aspx.cs" Inherits="Kalitte.RiskManagement.Web.Reports.Pages.BirimKarneDashboard" %>

<%@ Register Assembly="Kalitte.Dashboard.Framework" Namespace="Kalitte.Dashboard.Framework"
    TagPrefix="kalitte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentHolder" runat="server" >
    <kalitte:DashboardSurface ID="surface" runat="server" DashboardKey="40a8a28a-7abe-4861-9167-75aaf641c1b1" >
    </kalitte:DashboardSurface>
</asp:Content>
