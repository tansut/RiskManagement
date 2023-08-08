<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DefaultMaster.Master"
    AutoEventWireup="true" CodeBehind="ViewDashboard.aspx.cs" Inherits="Kalitte.RiskManagement.Web.Pages.Dynamic.ViewDashboard" %>

<%@ Register Assembly="Kalitte.Dashboard.Framework" Namespace="Kalitte.Dashboard.Framework"
    TagPrefix="kalitte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentHolder" runat="server" >
    <kalitte:DashboardSurface ID="surface" runat="server" >
    </kalitte:DashboardSurface>
</asp:Content>
