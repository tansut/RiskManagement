<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Reports.WebUserControl1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)"  
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
    <LocalReport ReportPath="Reports\RiskReport.rdlc">
    </LocalReport>
</rsweb:ReportViewer>

