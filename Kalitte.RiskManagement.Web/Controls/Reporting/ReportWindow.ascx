<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportWindow.ascx.cs"
    Inherits="Kalitte.RiskManagement.Web.Controls.Reporting.ReportWindow" %>
<tt:TTWindow runat="server" ID="ctlRepWindow" Height="600" Width="750" Resizable="false" Maximizable="false" AutoScroll="false" >
    <AutoLoad MaskMsg="Yükleniyor ..." ShowMask="true" Mode="IFrame">
    </AutoLoad>
    <Content>
    </Content>
    <Buttons>
        <tt:TTButton ID="TTButton1" runat="server" Text="Kapat">
            <Listeners>
                <Click Handler="#{ctlRepWindow}.hide();" />
            </Listeners>
        </tt:TTButton>
    </Buttons>
</tt:TTWindow>
