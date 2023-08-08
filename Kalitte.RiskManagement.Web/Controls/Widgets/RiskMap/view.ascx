<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="view.ascx.cs" Inherits="Kalitte.RiskManagement.Web.Controls.Widgets.RiskMap.view" %>
<asp:UpdatePanel runat="server" ID="ctlUp" UpdateMode="Conditional">
    <ContentTemplate>
        <tt:TTPanel ID="TTPanel2" runat="server" Header="false" Padding="0" AutoHeight="true"
            BodyStyle="bacground-color:#ffffff">
            <Items>
                <tt:TTRiskScoreGrid runat="server" ID="ctlRiskMatrix" Layout="FitLayout" Header="false" CellSize="70"
                    Title="Risk Skor Matrisi">
                </tt:TTRiskScoreGrid>
            </Items>
        </tt:TTPanel>
    </ContentTemplate>
</asp:UpdatePanel>
