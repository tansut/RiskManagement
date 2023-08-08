<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapWidget.ascx.cs" Inherits="Kalitte.WidgetLibrary.MapVisualizer.MapWidget" %>
<%@ Register Assembly="WebControls" Namespace="WebControls" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cc1:SilverlightMap ID="ctlMap" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
